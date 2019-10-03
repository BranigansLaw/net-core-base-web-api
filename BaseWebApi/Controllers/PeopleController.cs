using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using prioritizemeServices.Core.Data;
using prioritizemeServices.Database;
using System.Threading.Tasks;

namespace prioritizemeServices.Controllers
{
    /// <summary>
    /// Controller for interacting with <see cref="Person"/> data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        /// <summary>
        /// The <see cref="PrioritizeMeDbContext"/> factory
        /// </summary>
        private IDesignTimeDbContextFactory<PrioritizeMeDbContext> _factory;

        /// <summary>
        /// Creates a new instance of this contructor
        /// </summary>
        /// <param name="factory"></param>
        public PeopleController(
            IDesignTimeDbContextFactory<PrioritizeMeDbContext> factory)
        {
            _factory = factory ?? throw new System.ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets a list of all the <see cref="Person"/>s in the database
        /// </summary>
        /// <returns>The list of all the <see cref="Person"/>s in the database</returns>
        [HttpGet]
        public async Task<OkObjectResult> Index()
        {
            Person[] allPeople = new Person[0];
            using (PrioritizeMeDbContext context = _factory.CreateDbContext(new string[0]))
            {
                allPeople = await context.People.ToArrayAsync();
            }

            return Ok(allPeople);
        }
    }
}