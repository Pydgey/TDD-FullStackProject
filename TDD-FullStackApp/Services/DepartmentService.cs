using TDD_FullStackApp.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TDD_FullStackApp.Services
{
    public class DepartmentService
    {
        private readonly MySqlConnectionStringBuilder connection;
        public DepartmentService()
        {
            connection = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = 3308,
                UserID = "root",
                Database = "mytestdb"
            };
        }

        public async Task<JsonResult> FindAll()
        {
            try
            {
                string query = @"select DepartmentId,DepartmentName from Department";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    await using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
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

        public async Task<Department> FindByName(string name)
        {
            try
            {
                string query = @"select DepartmentId, DepartmentName from department where DepartmentName = @name";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    await using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@name", name);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
                var res = table.Select();
                var id = res[0].ItemArray[0] ;
                var Depname = res[0].ItemArray[1];

                return new Department
                {
                    DepartmentId = (int)id, 
                    DepartmentName = Depname.ToString()
                };
                
            }
            catch
            {
                throw new Exception("Error FindByName");
            }
        }

        public async void CreateDepartment(Department department)
        {
            try
            {
                string query = @"insert into Department (DepartmentName) Values (@DepartmentName)";

                DataTable table = new DataTable();
                MySqlDataReader myReader;

                using (MySqlConnection mycon = new MySqlConnection(connection.ToString()))
                {
                    mycon.Open();
                    await using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        public Task<Department> UpdateDepartment(Department department)
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
                var depart = FindByName(department.DepartmentName);
                return depart;
            }
            catch
            {
                throw new Exception("Get Error");
            }
        }

        public JsonResult DeleteDepartment(int id)
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
            catch(Exception ex)
            {
                throw new Exception("Get Error", ex);
            }
        }
    }
}
