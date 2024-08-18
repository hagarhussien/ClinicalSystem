using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicalSystem.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Email { get; set; }
        public int password { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Diseases { get; set; }
        public string Phone { get; set; }

        [Display(Name = "Image")]
        [DefaultValue("default.png")]
        public string? imagefile { get; set; }

    }
}
