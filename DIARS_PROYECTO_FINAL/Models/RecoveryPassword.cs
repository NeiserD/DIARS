using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class RecoveryPassword
    {
        public string token { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string Password2 { get; set; }

    }
}