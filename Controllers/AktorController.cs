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
    public class AktorController : Controller
    {
        private readonly FilmyContext _context;

        public AktorController(FilmyContext context)
        {
            _context = context;
        }

        // GET: Aktor
        public async Task<IActionResult> Index()
        {
            //   return _context.Aktor != null ? 
            //               View(await _context.Aktor.ToListAsync()) :
            //               Problem("Entity set 'FilmyContext.Aktor'  is null.");
            var prac = _context.Aktor.Include(p => p.Films).AsNoTracking();
            return View(await prac.ToListAsync());
        }

        // GET: Aktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aktor == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor
                .Include(p => p.Films)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktor == null)
            {
                return NotFound();
            }

            return View(aktor);
        }

        // GET: Aktor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,DataUrodzenia,KrajPochodzenia")] Aktor aktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aktor);
        }

        // GET: Aktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aktor == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor.FindAsync(id);
            if (aktor == null)
            {
                return NotFound();
            }
            return View(aktor);
        }

        // POST: Aktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia,KrajPochodzenia")] Aktor aktor)
        {
            if (id != aktor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktorExists(aktor.Id))
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
            return View(aktor);
        }

        // GET: Aktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aktor == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktor == null)
            {
                return NotFound();
            }

            return View(aktor);
        }

        // POST: Aktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aktor == null)
            {
                return Problem("Entity set 'FilmyContext.Aktor'  is null.");
            }
            var aktor = await _context.Aktor.FindAsync(id);
            if (aktor != null)
            {
                _context.Aktor.Remove(aktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktorExists(int id)
        {
          return (_context.Aktor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
