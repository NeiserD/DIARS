using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Direcciones
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Distrito { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
    }
}