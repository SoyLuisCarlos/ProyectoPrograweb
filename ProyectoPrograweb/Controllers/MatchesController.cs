using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograweb.Models;
using ProyectoPrograweb.Models.dbModels;

namespace ProyectoPrograweb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatchesController : Controller
    {
        private readonly ProyectoContext _context;

        public MatchesController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Matches.Include(m => m.IdNewsCategoryNavigation);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.IdNewsCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdMatch == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatch,MatchTitle,MatchImage,MatchDescription,IdNewsCategory")] MatchHR match)
        {
            if (ModelState.IsValid)
            {
                Match partido = new Match
                {
                    IdMatch = match.IdMatch,
                    MatchTitle = match.MatchTitle,
                    MatchDescription = match.MatchDescription,
                    MatchImage = match.MatchImage,
                    IdNewsCategory = match.IdNewsCategory
                };
                _context.Add(partido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", match.IdNewsCategory);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", match.IdNewsCategory);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatch,MatchTitle,MatchImage,MatchDescription,IdNewsCategory")] MatchHR match)
        {
            if (id != match.IdMatch)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.IdMatch))
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
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", match.IdNewsCategory);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.IdNewsCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdMatch == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'ProyectoContext.Matches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return (_context.Matches?.Any(e => e.IdMatch == id)).GetValueOrDefault();
        }
    }
}