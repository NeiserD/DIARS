using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int cantidad { get; set; }
        public int IdUsuario {get;set;}
        public int IdProducto { get; set; }

        public Usuario Usuario { get; set; }

        public Producto Producto{ get; set; }
    }
}