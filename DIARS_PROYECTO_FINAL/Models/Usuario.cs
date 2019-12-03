using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Usuario
    {
        public Usuario()
        {
            ListaCarritos = new List<Carrito>();
        }

        public int Id { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }
        public int IdRol { get; set; }
        public string username{ get; set; }
        public string dni{ get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string celular { get; set; }
        public string TokenRecovery { get; set; }

       public  List <Carrito> ListaCarritos { get; set; }
    }
}