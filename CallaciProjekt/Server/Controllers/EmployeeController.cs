using CallaciProjekt.Server.DataAccess;
using CallaciProjekt.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallaciProjekt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeDataAccessLayer objEmployee = new EmployeeDataAccessLayer();
        [HttpGet]
        public Task<List<Employee>> Get()
        {
            return objEmployee.GetAllEmployees();
        }
        [HttpGet("{id}")]
        public Task<Employee> GetById(string id)
        {
            return objEmployee.GetEmployeeData(id);
        }
        [HttpPost("PostEmp")]
        public void Post([FromBody] Employee employee)
        {
            objEmployee.addEmployee(employee);
        }

        [HttpPut]
        public void Put([FromBody] Employee employee)
        {
            objEmployee.UpdateEmployee(employee);
        }

        [HttpDelete("{id}")]
        public void DeleteById(string id)
        {
            objEmployee.DeleteEmployee(id);
        }

        [HttpGet("GetCities")]
        public Task<List<Cities>> GetAllCities()
        {
            return objEmployee.GetCityData();
        }
    }
}
