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
    public class FilmController : Controller
    {
        private readonly FilmyContext _context;

        public FilmController(FilmyContext context)
        {
            _context = context;
        }

        // GET: Film
        public async Task<IActionResult> Index()
        {
            //   return _context.Film != null ? 
            //               View(await _context.Film.ToListAsync()) :
            //               Problem("Entity set 'FilmyContext.Film'  is null.");
            var prac = _context.Film.Include(p => p.Aktor).Include(p => p.Oceny).AsNoTracking();
            return View(await prac.ToListAsync());
        }

        // GET: Film/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(p => p.Aktor)
                .Include(p => p.Oceny)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        private void PopulateAktorDropDownList(object selectedAktor = null)
        {
            var aktorQuery = from d in _context.Aktor
                                   orderby d.Nazwisko // Sort by name.
                                   select d;
            var res = aktorQuery.AsNoTracking().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nazwisko + " " + a.Imie
            });
            ViewBag.AktorID = new SelectList(res, "Value", "Text", selectedAktor);
        }

        // GET: Film/Create
        public IActionResult Create()
        {
            PopulateAktorDropDownList();
            return View();
        }

        // POST: Film/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,RokProdukcji,Gatunek,Rezyser,Opis,Ocena")] Film film, IFormCollection form)
        {
            string aktorValue = form["Aktor"].ToString();
            if (ModelState.IsValid)
            {
                Aktor aktor = null;
                if (aktorValue != "-1")
                {
                    var aa = _context.Aktor.Where(a => a.Id == int.Parse(aktorValue));
                    if (aa.Count() > 0)
                    {
                        aktor = aa.First();
                    }
                }
                film.Aktor = aktor;

                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Film/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            // var film = await _context.Film.FindAsync(id);
            var film = _context.Film.Where(p => p.Id == id).Include(p => p.Aktor).First();
            if (film == null)
            {
                return NotFound();
            }
            if (film.Aktor != null)
            {
                PopulateAktorDropDownList(film.Aktor.Id);
            }
            else
            {
                PopulateAktorDropDownList();
            }
            return View(film);
        }

        // POST: Film/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,RokProdukcji,Gatunek,Rezyser,Opis,Ocena")] Film film, IFormCollection form)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string aktorValue = form["Aktor"];

                    Aktor aktor = null;
                    if (aktorValue != "-1")
                    {
                        var aa = _context.Aktor.Where(a => a.Id == int.Parse(aktorValue));
                        if (aa.Count() > 0)
                        {
                            aktor = aa.First();
                        }
                    }
                    film.Aktor = aktor;

                    Film ff = _context.Film.Where(p => p.Id == id)
                    .Include(p => p.Aktor)
                    .First();
                    ff.Tytul = film.Tytul;
                    ff.RokProdukcji = film.RokProdukcji;
                    ff.Gatunek = film.Gatunek;
                    ff.Rezyser = film.Rezyser;
                    ff.Opis = film.Opis;
                    ff.Oceny = film.Oceny;
                    ff.Aktor = film.Aktor;
                    

                    // _context.Update(film);
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
            return View(film);
        }

        // GET: Film/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            var film = _context.Film.Where(p => p.Id == id).Include(p => p.Aktor).First();
            // var film = await _context.Film
            //     .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Film == null)
            {
                return Problem("Entity set 'FilmyContext.Film'  is null.");
            }
            var film = await _context.Film.FindAsync(id);
            if (film != null)
            {
                _context.Film.Remove(film);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
          return (_context.Film?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
