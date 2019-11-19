using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class DireccionesMap:EntityTypeConfiguration<Direcciones>
    {
        public DireccionesMap()
        {
            ToTable("Direcciones");
            HasKey(a => a.Id);
        }
    }
}