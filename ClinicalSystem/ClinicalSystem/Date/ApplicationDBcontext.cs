using ClinicalSystem.Models;
using ClinicalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalSystem.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options) { }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Doctor> Patient { get; set; }
        public DbSet<ClinicalSystem.Models.Patient>? Patient_1 { get; set; }

    }
}