﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class GemForm
    {
        public Guid IconId { get; set; }
        [Display(Name = "name", Description = "Gem name (optional)")]
        public string Name { get; set; }
        [Display(Name = "description", Description = "Gem description (optional, maximum 100 characters)")]
        public string Description { get; set; }
        [Display(Name = "address", Description = "Full street address (optional)")]
        public string Address { get; set; }
        [Required]
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string YelpUrl { get; set; }
        public string GoogleUrl { get; set; }
        public string MenuUrl { get; set; }
    }
}
