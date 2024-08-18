using ClinicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ClinicalSystem.Data;
using System.Diagnostics.Metrics;
using ClinicalSystem.Migrations;

namespace ClinicalSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDBcontext _context;



        public LoginController(ApplicationDBcontext context)
        {
            this._context = context;

        }
        [HttpGet]
        public IActionResult Index10()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index10(PatientLogin model)
        {
            if (ModelState.IsValid)
            {

                var user = _context.Patient.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && model.password == model.password)
                {
                    // HttpContext.Session.SetString("name", model.ID);

                    return RedirectToAction("Index9", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid email");
                }



            }

        return RedirectToAction("Index9" , "Home");
        }
    }
}
