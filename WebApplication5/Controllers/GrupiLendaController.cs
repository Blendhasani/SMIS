using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Data.ViewModel;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GrupiLendaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrupiLendaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrupiLenda
        public async Task<IActionResult> Index()
        {
              return _context.GrupiLenda!= null ? 
                          View(await _context.GrupiLenda.Include(x=>x.Grupi).Include(x=>x.Subject).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GrupiLenda'  is null.");
        }

        // GET: GrupiLenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GrupiLenda == null)
            {
                return NotFound();
            }

            var grupiLenda = await _context.GrupiLenda.Include(x => x.Grupi).Include(x => x.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupiLenda == null)
            {
                return NotFound();
            }

            return View(grupiLenda);
        }

        // GET: GrupiLenda/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["GrupiId"] = new SelectList(_context.Grupet, "Id", "Emri");
            return View();
        }

        // POST: GrupiLenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrupiLendaVM grupiLenda)
        {
             
            if (!ModelState.IsValid)
            {
            return View(grupiLenda);
            }
            var gl = new GrupiLenda()
            {
                SubjectId = grupiLenda.SubjectId,
                GrupiId = grupiLenda.GrupiId
            };
                _context.Add(gl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: GrupiLenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["GrupiId"] = new SelectList(_context.Grupet, "Id", "Emri");
            if (id == null || _context.GrupiLenda == null)
            {
                return NotFound();
            }

            var grupiLenda = await _context.GrupiLenda.FindAsync(id);
            if (grupiLenda == null)
            {
                return NotFound();
            }
            return View(grupiLenda);
        }

        // POST: GrupiLenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GrupiId,SubjectId")] GrupiLenda grupiLenda)
        {
            if (id != grupiLenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupiLenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupiLendaExists(grupiLenda.Id))
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
            return View(grupiLenda);
        }

        // GET: GrupiLenda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GrupiLenda == null)
            {
                return NotFound();
            }

            var grupiLenda = await _context.GrupiLenda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupiLenda == null)
            {
                return NotFound();
            }

            return View(grupiLenda);
        }

        // POST: GrupiLenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GrupiLenda == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GrupiLenda'  is null.");
            }
            var grupiLenda = await _context.GrupiLenda.FindAsync(id);
            if (grupiLenda != null)
            {
                _context.GrupiLenda.Remove(grupiLenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupiLendaExists(int id)
        {
          return (_context.GrupiLenda?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
