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
using Microsoft.Win32;

namespace ClinicalSystem.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDBcontext _context;
        private readonly IWebHostEnvironment _environment;
        public DoctorsController(ApplicationDBcontext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
              return _context.Doctor != null ? 
                          View(await _context.Doctor.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBcontext.Doctor'  is null.");
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Email == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,username,password,Specialty,Age,Phone,imagefile")] Doctor doctor , IFormFile imagefile)
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
                    doctor.imagefile = imagefile.FileName;
                }
            }
            else
            {
                doctor.imagefile = "default.jpeg"; // to save the default image path in database.
            }
            try
            {
                _context.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index1", "account");
            }
            catch (Exception ex) { ViewBag.exc = ex.Message; }




            return View();
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,username,password,Specialty,Age,Phone,imagefile")] Doctor doctor)
        {
            if (id != doctor.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Email))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Email == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Doctor == null)
            {
                return Problem("Entity set 'ApplicationDBcontext.Doctor'  is null.");
            }
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctor.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(string id)
        {
          return (_context.Doctor?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
