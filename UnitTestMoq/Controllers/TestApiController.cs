using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UnitTestMoq.Domain.Customers;
using UnitTestMoq.Models;

namespace UnitTestMoq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestApiController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/TestApi
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult<Customer>> PostCustomer(MyModelWrapper json)
        {
            return CreatedAtAction("GetCustomer", new { id = 1 });
        }
    }
}