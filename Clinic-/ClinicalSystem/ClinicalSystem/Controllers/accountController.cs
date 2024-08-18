using ClinicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ClinicalSystem.Data;
using System.Diagnostics.Metrics;


namespace ClinicalSystem.Controllers
{
    public class accountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDBcontext _context;



        public accountController(ApplicationDBcontext context)
        {
            this._context = context;

        }
        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index1(DoctorLogin model)
        {
            if (ModelState.IsValid)
            {

                var user = _context.Doctor.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && model.password == model.password)
                {
                    // HttpContext.Session.SetString("name", model.ID);

                    return RedirectToAction("index6", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid email");
                }



            }

            return RedirectToAction("index6", "Home");
        }
    }
}
