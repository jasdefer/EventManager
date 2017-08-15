﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model.Visitors
{
    public class UpdateVisitorModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(1024)]
        public string Bio { get; set; }
    }
}
