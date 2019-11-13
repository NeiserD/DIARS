using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class MetodoPagoMap:EntityTypeConfiguration<MetodoPago>
    {
        public MetodoPagoMap()
        {
            ToTable("MetodoPago");
            HasKey(a=>a.id);
        }
    }
}