using Microsoft.AspNetCore.Mvc;
using TDD_FullStackApp.Models;
using MySql.Data.MySqlClient;
using System.Data;
using TDD_FullStackApp.Services;

namespace TDD_FullStackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly MySqlConnectionStringBuilder connection;
        private readonly DepartmentService service;
        public DepartmentController()
        {
            service = new DepartmentService();
            connection = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = 3308,
                UserID = "root",
                Database = "mytestdb"
            };
        }

        [HttpGet]
        public Task<JsonResult> Get()
        {
            try
            {
                var results = service.FindAll();
                return results;
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        [HttpGet("{name}")]
        public Task<Department> GetByName([FromRoute] string name)
        {
            try
            {
                var results = service.FindByName(name);
                return results;
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        [HttpPost]
        public JsonResult Post(Department department)
        {
            try
            {
                service.CreateDepartment(department);
                return new JsonResult("Added Successfully");
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        [HttpPut]
        public Task<Department> Put(Department department)
        {
            try
            {
                var departmentUpdated = service.UpdateDepartment(department);
                return departmentUpdated;
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                var deletedResponse = service.DeleteDepartment(id);
                return deletedResponse;
            }
            catch
            {
                throw new Exception("Get Error");
            }

        }
    }
}
