using System.Threading.Tasks;
using UnitTestMoq.Models;

namespace UnitTestMoq.Services
{
    public interface IEmployeeService
    {
        Task<string> GetEmployeeNameById(int EmpID);

        Task<Employee> GetEmployeeDetails(int EmpID);
    }
}