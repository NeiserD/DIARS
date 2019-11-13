using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class OfertaMap:EntityTypeConfiguration<Oferta>
    {
        public OfertaMap()
        {
            ToTable("Oferta");
            HasKey(o=>o.id);
        }
    }
}