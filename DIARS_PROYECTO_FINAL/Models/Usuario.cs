﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int IdRol { get; set; }
        public string username{ get; set; }
        public string dni{ get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string celular { get; set; }
    }
}