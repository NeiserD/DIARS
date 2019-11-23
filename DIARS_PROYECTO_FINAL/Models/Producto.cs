using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public decimal precio{ get; set; }
        public int idCategoria{ get; set; }
        public int stock { get; set; }
        public bool isActive { get; set; }
        public string imagen { get; set; }
        public Categoria Categoria { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int talla { get; set; }
        

    }
}