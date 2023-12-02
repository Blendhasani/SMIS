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
    public class GrupiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrupiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grupi
        public async Task<IActionResult> Index()
        {
              return _context.Grupet != null ? 
                          View(await _context.Grupet.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Grupet'  is null.");
        }

        // GET: Grupi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grupet == null)
            {
                return NotFound();
            }

            var grupi = await _context.Grupet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupi == null)
            {
                return NotFound();
            }

            return View(grupi);
        }

        // GET: Grupi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri")] Grupi grupi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupi);
        }

        // GET: Grupi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grupet == null)
            {
                return NotFound();
            }

            var grupi = await _context.Grupet.FindAsync(id);
            if (grupi == null)
            {
                return NotFound();
            }
            return View(grupi);
        }

        // POST: Grupi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri")] Grupi grupi)
        {
            if (id != grupi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupiExists(grupi.Id))
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
            return View(grupi);
        }

        // GET: Grupi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grupet == null)
            {
                return NotFound();
            }

            var grupi = await _context.Grupet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupi == null)
            {
                return NotFound();
            }

            return View(grupi);
        }

        // POST: Grupi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grupet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grupet'  is null.");
            }
            var grupi = await _context.Grupet.FindAsync(id);
            if (grupi != null)
            {
                _context.Grupet.Remove(grupi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupiExists(int id)
        {
          return (_context.Grupet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
