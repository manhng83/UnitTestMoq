using Moq;
using UnitTestMoq.Controllers;
using UnitTestMoq.Models;
using UnitTestMoq.Services;
using Xunit;

namespace UnitTesting
{
    public class EmployeeTest
    {
        #region Property

        public Mock<IEmployeeService> mock = new Mock<IEmployeeService>();

        #endregion Property

        [Fact]
        public async void GetEmployeeNameById()
        {
            mock.Setup(p => p.GetEmployeeNameById(1)).ReturnsAsync("JK");
            EmployeeController emp = new EmployeeController(mock.Object);
            string result = await emp.GetEmployeeById(1);
            Assert.Equal("JK", result);
        }

        [Fact]
        public async void GetEmployeeDetails()
        {
            var employeeDTO = new Employee()
            {
                Id = 1,
                Name = "JK",
                Desgination = "SDE"
            };
            mock.Setup(p => p.GetEmployeeDetails(1)).ReturnsAsync(employeeDTO);
            EmployeeController emp = new EmployeeController(mock.Object);
            var result = await emp.GetEmployeeDetails(1);
            Assert.True(employeeDTO.Equals(result));
        }
    }
}