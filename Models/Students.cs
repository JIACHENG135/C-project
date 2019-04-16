using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Students
    {
        public string Id { get; set; }
        [Required]
        [StringLength(10,ErrorMessage="Your String is too long")]
        [DisplayName("Enter your name")]
        public string Name { get; set; }

        public string Telephone { get; set; }
    }
}
