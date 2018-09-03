﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElGasCamion.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string IdAspNetUser { get; set; }
        public string DeviceID { get; set; }
        public bool Habilitado { get; set; }
        public int? IdSector { get; set; }
    }
}
