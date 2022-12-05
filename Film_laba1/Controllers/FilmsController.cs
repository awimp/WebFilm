using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Film_laba1;
using Film_laba1.Models;

namespace Film_laba1.Controllers
{
    public class FilmsController : Controller
    {
        private readonly DBFilmsContext _context;

        public FilmsController(DBFilmsContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Genres", "Index");
            ViewBag.GenreId = id;
            ViewBag.GenreName = name;
            var filmsByGenre = _context.Films.Where(p => p.GenreId == id).Include(p => p.Genre).Include(p => p.FilmCompany).Include(p => p.Producer);
            return View(await filmsByGenre.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.FilmCompany)
                .Include(f => f.Genre)
                .Include(f => f.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create(int ProducerId)
        {
            ViewBag.ProducerName = _context.Producers.Where(c => c.Id == ProducerId).FirstOrDefault().Name;
            ViewData["FilmCompanyId"] = new SelectList(_context.Filmcompanies, "Id", "Id");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Release,GenreId,ProducerId,Info,FilmCompanyId")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmCompanyId"] = new SelectList(_context.Filmcompanies, "Id", "Id", film.FilmCompanyId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", film.ProducerId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["FilmCompanyId"] = new SelectList(_context.Filmcompanies, "Id", "Id", film.FilmCompanyId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", film.ProducerId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Release,GenreId,ProducerId,Info,FilmCompanyId")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmCompanyId"] = new SelectList(_context.Filmcompanies, "Id", "Id", film.FilmCompanyId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Id", film.ProducerId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.FilmCompany)
                .Include(f => f.Genre)
                .Include(f => f.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'DBFilmsContext.Films'  is null.");
            }
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
          return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
