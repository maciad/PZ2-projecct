using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Filmy.Data;
using Filmy.Models;

namespace Filmy.Controllers
{
    public class OcenaController : Controller
    {
        private readonly FilmyContext _context;

        public OcenaController(FilmyContext context)
        {
            _context = context;
        }

        private void PopulateFilmDropDownList(object selectedFilm = null)
        {
            var filmQuery = from d in _context.Film
                                   orderby d.Tytul // Sort by name.
                                   select d;

            ViewBag.FilmId = new SelectList(filmQuery.AsNoTracking(), "Id", "Tytul", selectedFilm);
        }

        private void PopulateUzytkownikDropDownList(object selectedUzytkownik = null)
        {
            var uzytkownikQuery = from d in _context.Uzytkownik
                                   orderby d.Nazwisko, d.Imie // Sort by name.
                                   select d;

            ViewBag.UzytkownikId = new SelectList(uzytkownikQuery.AsNoTracking(), "Id", "Email", selectedUzytkownik);
        }

        // GET: Ocena
        public async Task<IActionResult> Index()
        {
            //   return _context.Ocena != null ? 
            //               View(await _context.Ocena.ToListAsync()) :
            //               Problem("Entity set 'FilmyContext.Ocena'  is null.");
            var prac = _context.Ocena.Include(p => p.Film).Include(p => p.Uzytkownik).AsNoTracking();
            return View(await prac.ToListAsync());
        }

        // GET: Ocena/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ocena == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena
                .Include(p => p.Film)
                .Include(p => p.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // GET: Ocena/Create
        public IActionResult Create()
        {
            PopulateFilmDropDownList();
            PopulateUzytkownikDropDownList();
            return View();
        }

        // POST: Ocena/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OcenaWartosc")] Ocena ocena, IFormCollection form)
        {
            // Console.WriteLine("Ocena.Create: " + form["OcenaWartosc"]);
            string filmId = form["Film"];
            if (ModelState.IsValid)
            {
                Film film = null;
                var ff = _context.Film.Where(f => f.Id == int.Parse(filmId));
                if (ff.Count() > 0)
                {
                    film = ff.First();
                }
                ocena.Film = film;
                Uzytkownik u = null;
                var uu = _context.Uzytkownik.Where(f => f.Id == 1);
                if (uu.Count() > 0)
                {
                    u = uu.First();
                }
                ocena.Uzytkownik = u;

                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocena);
        }

        // GET: Ocena/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ocena == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena.FindAsync(id);
            if (ocena == null)
            {
                return NotFound();
            }
            return View(ocena);
        }

        // POST: Ocena/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OcenaWartosc")] Ocena ocena)
        {
            if (id != ocena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.Id))
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
            return View(ocena);
        }

        // GET: Ocena/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ocena == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // POST: Ocena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ocena == null)
            {
                return Problem("Entity set 'FilmyContext.Ocena'  is null.");
            }
            var ocena = await _context.Ocena.FindAsync(id);
            if (ocena != null)
            {
                _context.Ocena.Remove(ocena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcenaExists(int id)
        {
          return (_context.Ocena?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
