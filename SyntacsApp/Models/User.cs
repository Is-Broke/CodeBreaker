using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    public class User
    {
        public int ID { get; set; }
        [StringLength(20, MinimumLength = 3), Required]
        public string Alias { get; set; }
    }
}
