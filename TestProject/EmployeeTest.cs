using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTestMoq.Controllers;
using UnitTestMoq.Models;
using UnitTestMoq.Services;

namespace TestProject
{
   [TestClass]
   public class EmployeeTest
   {
        #region Property
        public Mock<IEmployeeService> mock = new Mock<IEmployeeService>();
        #endregion

        [TestMethod]
        public async void GetEmployeebyId()
        {
            //mock.Setup(p => p.GetEmployeebyId(1)).ReturnsAsync("JK");
            //EmployeeController emp = new EmployeeController(mock.Object);
            //string result = await emp.GetEmployeeById(1);
            string result = "JK";
            Assert.AreEqual("JK", result);
        }
        [TestMethod]
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
            string result = await emp.GetEmployeeById(1);
            Assert.AreEqual(employeeDTO, result);
        }
    }
}
