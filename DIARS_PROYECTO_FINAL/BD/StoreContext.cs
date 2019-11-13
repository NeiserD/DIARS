using DIARS_PROYECTO_FINAL.BD.Maps;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD
{
    public class StoreContext:DbContext
    {
        public IDbSet<Categoria> Categorias { get; set; }
        public IDbSet<MetodoPago> metodoPagos { get; set; }
        public IDbSet<Oferta> ofertas { get; set; }
        public IDbSet<Usuario>  Usuarios{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new MetodoPagoMap());
            modelBuilder.Configurations.Add(new OfertaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
        }
    }
    
}