using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film_laba1.Models
{
    public partial class Filmcompany
    {
        public Filmcompany()
        {
            Films = new HashSet<Film>();
        }

        public int Id { get; set; }
        [Display(Name = "Кінокомпанія")]
        public string? Name { get; set; }
        [Display(Name = "Інформація")]
        public string? Info { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
