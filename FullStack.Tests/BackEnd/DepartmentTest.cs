using fullstackBackEnd.Controllers;
using fullstackBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.BackEnd
{
    public class DepartmentTest : IDisposable
    {
        private readonly DepartmentController _controller;
        public ITestOutputHelper SaidaConsole;
        public DepartmentTest ( ITestOutputHelper _saidaConsole)
        {
            _controller = new DepartmentController();
            SaidaConsole = _saidaConsole;
            SaidaConsole.WriteLine("Constructor invocado", _controller);
        }

        [Fact]
        public void TestGet()
        {
            var results = _controller.Get();
            SaidaConsole.WriteLine(results.StatusCode.GetValueOrDefault().ToString());
            Assert.IsType<JsonResult>(results);
        }

        [Fact]
        public void TestPost()
        {
            Department department = new Department();
            department.DepartmentName = "TesteQA";
            var results = _controller.Post(department);
            Assert.IsType<JsonResult>(results);
        }

        [Fact]
        public void TestPut()
        {
            Department department = new Department();
            department.DepartmentName = "TesteXunit";
            department.DepartmentId = 5;
            var results = _controller.Put(department);
            Assert.IsType<JsonResult>(results);
        }

        [Fact]
        public void TestDelete()
        {
            int departmentId = 5;
            var results = _controller.Delete(departmentId);
            Assert.IsType<JsonResult>(results);
        }
        public void Dispose()
        {
            SaidaConsole.WriteLine("Dispose");
        }
    }
}
