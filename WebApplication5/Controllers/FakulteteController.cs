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
    public class FakulteteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FakulteteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fakultete
        public async Task<IActionResult> Index()
        {
              return View(await _context.Fakultetet.ToListAsync());
        }

        // GET: Fakultete/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fakultetet == null)
            {
                return NotFound();
            }

            var fakulteti = await _context.Fakultetet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakulteti == null)
            {
                return NotFound();
            }

            return View(fakulteti);
        }

        // GET: Fakultete/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fakultete/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kodi,Emri,Shkurtesa")] Fakulteti fakulteti)
        {
           
                _context.Add(fakulteti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
        }

        // GET: Fakultete/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fakultetet == null)
            {
                return NotFound();
            }

            var fakulteti = await _context.Fakultetet.FindAsync(id);
            if (fakulteti == null)
            {
                return NotFound();
            }
            return View(fakulteti);
        }

        // POST: Fakultete/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kodi,Emri,Shkurtesa")] Fakulteti fakulteti)
        {
            if (id != fakulteti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fakulteti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FakultetiExists(fakulteti.Id))
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
            return View(fakulteti);
        }

        // GET: Fakultete/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fakultetet == null)
            {
                return NotFound();
            }

            var fakulteti = await _context.Fakultetet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakulteti == null)
            {
                return NotFound();
            }

            return View(fakulteti);
        }

        // POST: Fakultete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fakultetet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fakultetet'  is null.");
            }
            var fakulteti = await _context.Fakultetet.FindAsync(id);
            if (fakulteti != null)
            {
                _context.Fakultetet.Remove(fakulteti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FakultetiExists(int id)
        {
          return _context.Fakultetet.Any(e => e.Id == id);
        }
    }
}
