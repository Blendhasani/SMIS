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
    public class AfatisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AfatisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Afatis
        public async Task<IActionResult> Index()
        {
              return View(await _context.Afati.ToListAsync());
        }

        // GET: Afatis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Afati == null)
            {
                return NotFound();
            }

            var afati = await _context.Afati
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afati == null)
            {
                return NotFound();
            }

            return View(afati);
        }

        // GET: Afatis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Afatis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hapur,Rregullt")] Afati afati)
        {
            
                _context.Add(afati);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Afatis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Afati == null)
            {
                return NotFound();
            }

            var afati = await _context.Afati.FindAsync(id);
            if (afati == null)
            {
                return NotFound();
            }
            return View(afati);
        }

        // POST: Afatis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hapur,Rregullt")] Afati afati)
        {
            if (id != afati.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(afati);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfatiExists(afati.Id))
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

        // GET: Afatis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Afati == null)
            {
                return NotFound();
            }

            var afati = await _context.Afati
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afati == null)
            {
                return NotFound();
            }

            return View(afati);
        }

        // POST: Afatis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Afati == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Afati'  is null.");
            }
            var afati = await _context.Afati.FindAsync(id);
            if (afati != null)
            {
                _context.Afati.Remove(afati);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfatiExists(int id)
        {
          return _context.Afati.Any(e => e.Id == id);
        }
    }
}
