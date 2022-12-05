using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film_laba1.Models
{
    public partial class Country
    {
        public Country()
        {
            Producers = new HashSet<Producer>();
        }

        public int Id { get; set; }
        [Display(Name = "Країна")]
        public string? Name { get; set; }

        public virtual ICollection<Producer> Producers { get; set; }
    }
}
