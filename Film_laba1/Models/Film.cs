using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film_laba1.Models
{
    public partial class Film
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string? Name { get; set; }
        [Display(Name = "Дата випуску")]
        public int? Release { get; set; }
        [Display(Name = "Жанр")]
        public int? GenreId { get; set; }

        [Display(Name = "Режисер")]
        public int ProducerId { get; set; }
        [Display(Name = "Інформація")]
        public string? Info { get; set; }
        [Display(Name = "Кінокомпанія")]
        public int FilmCompanyId { get; set; }

        public virtual Filmcompany FilmCompany { get; set; } = null!;
        public virtual Genre? Genre { get; set; }
        public virtual Producer Producer { get; set; } = null!;
    }
}
