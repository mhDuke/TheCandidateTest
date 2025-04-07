using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite("Data Source=candidates.dat"));
builder.Services.AddTransient<DbInitiator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbInititor = scope.ServiceProvider.GetRequiredService<DbInitiator>();
await dbInititor.EnsureCreated();

app.Run();


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>().HasData(
        [
            new Candidate { Id = 1, EnglishName = "Alice Smith", ArabicName = "arabic name 1", ExperienceYears = 1 },
            new Candidate { Id = 2, EnglishName = "Bob Johnson", ArabicName = "arabic name 2", ExperienceYears = 2 },
            new Candidate { Id = 3, EnglishName = "Charlie Brown", ArabicName = "arabic name 3", ExperienceYears = 3 },
            new Candidate { Id = 4, EnglishName = "Mathew scrows", ArabicName = "arabic name 4", ExperienceYears = 3 },
        ]);
    }
    public DbSet<Candidate> Candidates { get; set; }
}

public class Candidate
{
    public int Id { get; set; }
    public string EnglishName { get; set; }
    public string ArabicName { get; set; }
    public int ExperienceYears { get; set; }
}

public class DbInitiator(ApplicationDbContext db)
{
    public async Task EnsureCreated()
    {
        //if (await db.Database.CanConnectAsync())
        //    return;

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}