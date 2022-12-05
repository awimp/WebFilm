using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film_laba1.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage  = "Поле має бути заповненим!")]
        [Display(Name = "Жанр")]
        public string? Name { get; set; }
        
        [Display(Name = "Інформація")]
        public string? Info { get; set; }
        

        public virtual ICollection<Film> Films { get; set; }
    }
}
