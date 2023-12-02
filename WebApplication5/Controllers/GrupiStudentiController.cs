using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GrupiStudentiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrupiStudentiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrupiStudenti
        public async Task<IActionResult> Index()
        {
              return _context.GrupiStudentet != null ? 
                          View(await _context.GrupiStudentet.Include(x => x.Grupi).Include(x => x.Student).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GrupiStudentet'  is null.");
        }

        // GET: GrupiStudenti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GrupiStudentet == null)
            {
                return NotFound();
            }

            var grupiStudenti = await _context.GrupiStudentet.Include(x => x.Grupi).Include(x => x.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupiStudenti == null)
            {
                return NotFound();
            }

            return View(grupiStudenti);
        }

        // GET: GrupiStudenti/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["GrupiId"] = new SelectList(_context.Grupet, "Id", "Emri");
            return View();
        }

        // POST: GrupiStudenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GrupiId,StudentId")] GrupiStudenti grupiStudenti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupiStudenti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupiStudenti);
        }

        // GET: GrupiStudenti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["GrupiId"] = new SelectList(_context.Grupet, "Id", "Emri");
            if (id == null || _context.GrupiStudentet == null)
            {
                return NotFound();
            }

            var grupiStudenti = await _context.GrupiStudentet.FindAsync(id);
            if (grupiStudenti == null)
            {
                return NotFound();
            }
            return View(grupiStudenti);
        }

        // POST: GrupiStudenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GrupiId,StudentId")] GrupiStudenti grupiStudenti)
        {
            if (id != grupiStudenti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupiStudenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupiStudentiExists(grupiStudenti.Id))
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
            return View(grupiStudenti);
        }

        // GET: GrupiStudenti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GrupiStudentet == null)
            {
                return NotFound();
            }

            var grupiStudenti = await _context.GrupiStudentet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupiStudenti == null)
            {
                return NotFound();
            }

            return View(grupiStudenti);
        }

        // POST: GrupiStudenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GrupiStudentet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GrupiStudentet'  is null.");
            }
            var grupiStudenti = await _context.GrupiStudentet.FindAsync(id);
            if (grupiStudenti != null)
            {
                _context.GrupiStudentet.Remove(grupiStudenti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupiStudentiExists(int id)
        {
          return (_context.GrupiStudentet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
