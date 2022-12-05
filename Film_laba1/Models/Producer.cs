using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film_laba1.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Films = new HashSet<Film>();
        }

        public int Id { get; set; }

        [Display(Name = "Режисер")]
        public string? Name { get; set; }
        [Display(Name = "Відомості")]
        public string? Info { get; set; }
        [Display(Name = "Батьківщина")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Film> Films { get; set; }
    }
}
