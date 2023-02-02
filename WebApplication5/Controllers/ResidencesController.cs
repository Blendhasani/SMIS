using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Controllers
{
    public class ResidencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Residences
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 10;
            var applicationDbContext = _context.Residences.Include(r => r.State);
            return View(applicationDbContext.ToPagedList(pageNumber, pageSize));
        }

        // GET: Residences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Residences == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences
                .Include(r => r.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residence == null)
            {
                return NotFound();
            }

            return View(residence);
        }

        // GET: Residences/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            return View();
        }

        // POST: Residences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Kodi,StateId")] Residence residence)
        {
           
                _context.Add(residence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
        }

        // GET: Residences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Residences == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", residence.StateId);
            return View(residence);
        }

        // POST: Residences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Kodi,StateId")] Residence residence)
        {
            if (id != residence.Id)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(residence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceExists(residence.Id))
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

        // GET: Residences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Residences == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences
                .Include(r => r.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residence == null)
            {
                return NotFound();
            }

            return View(residence);
        }

        // POST: Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Residences == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Residences'  is null.");
            }
            var residence = await _context.Residences.FindAsync(id);
            if (residence != null)
            {
                _context.Residences.Remove(residence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceExists(int id)
        {
          return _context.Residences.Any(e => e.Id == id);
        }
    }
}
