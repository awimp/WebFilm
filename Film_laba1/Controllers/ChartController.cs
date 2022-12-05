using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Film_laba1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DBFilmsContext _context;
        public ChartController(DBFilmsContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var genres = _context.Genres.Include(m=>m.Films).ToList();
            List<object> genFilm = new List<object>();
            genFilm.Add(new[] { "Жанр", "Кілкість фільмів"});
            foreach (var c in genres)
            {
                genFilm.Add(new object[] { c.Name, c.Films.Count() });
            }
            return new JsonResult(genFilm);
        }
    }
}
