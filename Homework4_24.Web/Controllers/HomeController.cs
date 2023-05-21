using Homework4_24.Data;
using Homework4_24.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Homework4_24.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=PeopleList;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var repo = new PeopleRepo(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.AddPerson(person);
           
        }

        [HttpPost]
        public void DeletePerson(int personId)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.DeletePersonById(personId);
        }

        public IActionResult ShowEditPerson(int personId)
        {
            var repo = new PeopleRepo(_connectionString);
            var person = repo.GetPersonById(personId);
            return Json(person);
       
        }

        [HttpPost]
        public void UpdatePerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.EditPerson(person);

        }

        

    }
}