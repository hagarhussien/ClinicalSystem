using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicalSystem.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Email { get; set; }
        public string username { get; set; }
       
        public int password { get; set; }
        public string Specialty { get; set; }
        public int Age { get; set; }
        public int Phone { get; set; }


        [Display(Name = "Image")]
        [DefaultValue("default.png")]
        public string? imagefile { get; set; }

    }
}
