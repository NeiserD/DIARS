using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class CarritoMap : EntityTypeConfiguration<Carrito>
    {
        public CarritoMap()
        {
            ToTable("Carrito");
            HasKey(a => a.Id);

            HasRequired(a => a.Usuario).WithMany(a => a.ListaCarritos).HasForeignKey(a => a.IdUsuario);


            HasRequired(a => a.Producto).WithMany(a => a.ListaCarritos).HasForeignKey(a => a.IdProducto);
        }
                
    }
}