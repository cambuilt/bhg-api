using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bhg.Models
{
    public class IconForm
    {
        [Required]
        [Display(Name = "name", Description = "Icon name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "url", Description = "Icon URL from Cloudinary")]
        public string Url { get; set; }
    }
}
