﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Auto
{
    public class Role
    {
        [Required]
        public string RoleName { get; set; }
    }
}
