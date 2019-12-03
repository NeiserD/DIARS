using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD.Maps
{
    public class CarouselMap:EntityTypeConfiguration<Carousel>
    {
        public CarouselMap()
        {
            ToTable("Carousel");
            HasKey(a=>a.Id);
        }
    }
}