using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UnitTestMoq.Models;

namespace UnitTestMoq.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Property

        private readonly AppDbContext _appDbContext;

        #endregion Property

        #region Constructor

        public EmployeeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #endregion Constructor

        public async Task<string> GetEmployeeNameById(int EmpID)
        {
            var name = await _appDbContext.Employees.Where(c => c.Id == EmpID).Select(d => d.Name).FirstOrDefaultAsync();
            return name;
        }

        public async Task<Employee> GetEmployeeDetails(int EmpID)
        {
            var emp = await _appDbContext.Employees.FirstOrDefaultAsync(c => c.Id == EmpID);
            return emp;
        }
    }
}