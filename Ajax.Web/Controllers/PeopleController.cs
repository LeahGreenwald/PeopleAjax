using Ajax.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ajax.Web.Controllers
{
    public class PeopleController : Microsoft.AspNetCore.Mvc.Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Person person)
        {
            var db = new PersonDb(_connectionString);
            db.Add(person);
            return Json(person);
        }

        public IActionResult GetAll()
        {
            var db = new PersonDb(_connectionString);
            List<Person> ppl = db.GetAll();
            return Json(ppl);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var db = new PersonDb(_connectionString);
            db.Delete(Id);
            return Json(Id);
        }
        [HttpPost]
        public IActionResult Edit(int id, string firstName, string lastName, int age)
        {
            var db = new PersonDb(_connectionString);
            db.Edit(id, firstName, lastName, age);
            return Json(id);
        }
    }
}
