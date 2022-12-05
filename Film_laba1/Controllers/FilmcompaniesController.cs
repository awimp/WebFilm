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
    public class FilmcompaniesController : Controller
    {
        private readonly DBFilmsContext _context;

        public FilmcompaniesController(DBFilmsContext context)
        {
            _context = context;
        }

        // GET: Filmcompanies
        public async Task<IActionResult> Index()
        {
              return _context.Filmcompanies != null ? 
                          View(await _context.Filmcompanies.ToListAsync()) :
                          Problem("Entity set 'DBFilmsContext.Filmcompanies'  is null.");
        }

        // GET: Filmcompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filmcompanies == null)
            {
                return NotFound();
            }

            var filmcompany = await _context.Filmcompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filmcompany == null)
            {
                return NotFound();
            }

            return View(filmcompany);
        }

        // GET: Filmcompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmcompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info")] Filmcompany filmcompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmcompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmcompany);
        }

        // GET: Filmcompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filmcompanies == null)
            {
                return NotFound();
            }

            var filmcompany = await _context.Filmcompanies.FindAsync(id);
            if (filmcompany == null)
            {
                return NotFound();
            }
            return View(filmcompany);
        }

        // POST: Filmcompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info")] Filmcompany filmcompany)
        {
            if (id != filmcompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmcompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmcompanyExists(filmcompany.Id))
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
            return View(filmcompany);
        }

        // GET: Filmcompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filmcompanies == null)
            {
                return NotFound();
            }

            var filmcompany = await _context.Filmcompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filmcompany == null)
            {
                return NotFound();
            }

            return View(filmcompany);
        }

        // POST: Filmcompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filmcompanies == null)
            {
                return Problem("Entity set 'DBFilmsContext.Filmcompanies'  is null.");
            }
            var filmcompany = await _context.Filmcompanies.FindAsync(id);
            if (filmcompany != null)
            {
                _context.Filmcompanies.Remove(filmcompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmcompanyExists(int id)
        {
          return (_context.Filmcompanies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
