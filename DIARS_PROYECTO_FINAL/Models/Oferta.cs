using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Oferta
    {
     /*   public Oferta()
        {
            precioNormal = 1;
        }*/
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaInicio{ get; set; }
        public DateTime fechaFin { get; set; }
        public string descripcion{ get; set; }
        public string imagen { get; set; }
        public decimal precioNormal { get; set; }
        public decimal precioOferta { get; set; }
        public int porcentajeOferta { get; set; }
    }
}