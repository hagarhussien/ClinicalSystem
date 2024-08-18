using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicalSystem.Data;
using ClinicalSystem.Models;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace ClinicalSystem.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDBcontext _context;
        private readonly IWebHostEnvironment _environment;


        public PatientsController(ApplicationDBcontext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
              return _context.Patient_1 != null ? 
                          View(await _context.Patient_1.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBcontext.Patient_1'  is null.");
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Patient_1 == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient_1
                .FirstOrDefaultAsync(m => m.Email == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,password,PatientName,Address,Age,Diseases,Phone,imagefile")] Patient patient, IFormFile imagefile)
        {
            string path = Path.Combine(_environment.WebRootPath, "img"); // wwwroot/Img/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (imagefile != null)
            {
                path = Path.Combine(path, imagefile.FileName); // for exmple : /Img/Photoname.png
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imagefile.CopyToAsync(stream);
                    //ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", imag_file.FileName.ToString());
                    patient.imagefile = imagefile.FileName;
                }
            }
            else
            {
                patient.imagefile = "default.jpeg"; // to save the default image path in database.
            }
            try
            {
                _context.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index10" , "Login" );
            }
            catch (Exception ex) { ViewBag.exc = ex.Message; }




            return RedirectToAction( "Index10" , "Login" );
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Patient_1 == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient_1.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,password,PatientName,Address,Age,Diseases,Phone,imagefile")] Patient patient)
        {
            if (id != patient.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Patient_1 == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient_1
                .FirstOrDefaultAsync(m => m.Email == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Patient_1 == null)
            {
                return Problem("Entity set 'ApplicationDBcontext.Patient_1'  is null.");
            }
            var patient = await _context.Patient_1.FindAsync(id);
            if (patient != null)
            {
                _context.Patient_1.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
          return (_context.Patient_1?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
