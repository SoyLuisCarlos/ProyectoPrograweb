using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograweb.Models.dbModels;

namespace ProyectoPrograweb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsCategoriesController : Controller
    {
        private readonly ProyectoContext _context;

        public NewsCategoriesController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: NewsCategories
        public async Task<IActionResult> Index()
        {
              return _context.NewsCategories != null ? 
                          View(await _context.NewsCategories.ToListAsync()) :
                          Problem("Entity set 'ProyectoContext.NewsCategories'  is null.");
        }

        // GET: NewsCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewsCategories == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategories
                .FirstOrDefaultAsync(m => m.IdNewsCategory == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // GET: NewsCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNewsCategory,NewsCategoryDescription")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsCategory);
        }

        // GET: NewsCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewsCategories == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategories.FindAsync(id);
            if (newsCategory == null)
            {
                return NotFound();
            }
            return View(newsCategory);
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNewsCategory,NewsCategoryDescription")] NewsCategory newsCategory)
        {
            if (id != newsCategory.IdNewsCategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsCategoryExists(newsCategory.IdNewsCategory))
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
            return View(newsCategory);
        }

        // GET: NewsCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewsCategories == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategories
                .FirstOrDefaultAsync(m => m.IdNewsCategory == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // POST: NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewsCategories == null)
            {
                return Problem("Entity set 'ProyectoContext.NewsCategories'  is null.");
            }
            var newsCategory = await _context.NewsCategories.FindAsync(id);
            if (newsCategory != null)
            {
                _context.NewsCategories.Remove(newsCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsCategoryExists(int id)
        {
          return (_context.NewsCategories?.Any(e => e.IdNewsCategory == id)).GetValueOrDefault();
        }
    }
}
