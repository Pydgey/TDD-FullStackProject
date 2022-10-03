using fullstackBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace fullstackBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly MySqlConnectionStringBuilder connection;
        public DepartmentController ()
        {
            connection = new MySqlConnectionStringBuilder();
            connection.Server = "127.0.0.1";
            connection.Port = 3308;
            connection.UserID = "root";
            connection.Database = "mytestdb";
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"select DepartmentId,DepartmentName from Department";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

                return new JsonResult(table);
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
                string query = @"insert into Department (DepartmentName) Values (@DepartmentName)";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }
            catch
            {
                throw new Exception("Get Error");
            }

        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            try
            {
                string query = @"update Department set DepartmentName = @DepartmentName where DepartmentId = @DepartmentId";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                        myCommand.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
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
                string query = @"delete from Department where DepartmentId = @DepartmentId";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentId", id);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

                return new JsonResult("Deleted Successfully");
            }
            catch
            {
                throw new Exception("Get Error");
            }

        }
    }
}
