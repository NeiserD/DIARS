using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Recovery
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}