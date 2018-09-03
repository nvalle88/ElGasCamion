﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElGasCamion.Models
{
    public class DistribuidorResponse
    {
        public int IdDistribuidor { get; set; }
        public string Identificacion { get; set; }
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }    
    }
    public class DistribuidorFirebase
    {
        public int id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
