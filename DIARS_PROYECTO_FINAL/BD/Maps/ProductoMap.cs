using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class ProductoMap : EntityTypeConfiguration<Producto>
    {
        public ProductoMap()
        {
            ToTable("Producto");
            HasKey(a => a.Id);

            HasRequired(o => o.Categoria)
                .WithMany(o => o.Productos)
                .HasForeignKey(o=>o.idCategoria );
            
        }
        
    }
}