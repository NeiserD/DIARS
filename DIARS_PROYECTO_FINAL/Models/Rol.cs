using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Rol
    {
        public int Id{ get; set; }
        public int nombre { get; set; }
        public List<Usuario> Usuarios { get; set; } 
    }
}