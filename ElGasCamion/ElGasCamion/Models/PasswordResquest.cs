﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElGasCamion.Models
{
    public class PasswordRequest
    {
        public string Email { get; set; }

        public int Codigo { get; set; }

        public string NewPassword { get; set; }
    }
}
