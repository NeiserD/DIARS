using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Oferta
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaInicio{ get; set; }
        public DateTime fechaFin { get; set; }
        public string descripcion{ get; set; }

    }
}