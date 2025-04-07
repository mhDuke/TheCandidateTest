using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite("Data Source=candidates.dat"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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
            new Candidate { Id = 1, Name = "Alice Smith", ExperienceYears = 5 },
            new Candidate { Id = 2, Name = "Bob Johnson", ExperienceYears = 3 },
            new Candidate { Id = 3, Name = "Charlie Brown", ExperienceYears = 8 },
            new Candidate { Id = 4, Name = "Mathew scrows", ExperienceYears = 8 }
        );
    }
    public DbSet<Candidate> Candidates { get; set; }
}

public class Candidate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ExperienceYears { get; set; }
}