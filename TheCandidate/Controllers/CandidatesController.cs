using Microsoft.AspNetCore.Mvc;

namespace TheCandidate.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidatesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Candidate>> GetAllCandidates(ApplicationDbContext db) => Ok(db.Set<Candidate>());
}
