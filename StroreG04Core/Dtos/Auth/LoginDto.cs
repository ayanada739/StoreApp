﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is required!!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!!")]
        public string Password { get; set; }

    }
}
