﻿using System;
using System.Collections.Generic;
using System.Data;
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
    public class NewsController : Controller
    {
        private readonly ProyectoContext _context;

        public NewsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.News.Include(n => n.IdNewsCategoryNavigation).Include(n => n.IdUserNavigation);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.IdNewsCategoryNavigation)
                .Include(n => n.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdNews == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory");
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNews,NewsTitle,NewsDescription,NewsImage,NewsCreationDate,IdUser,IdNewsCategory")] NewsHR news)
        {
            if (ModelState.IsValid)
            {
                News noticia = new News
                {
                    NewsTitle = news.NewsTitle,
                    NewsDescription = news.NewsDescription,
                    NewsImage = news.NewsImage,
                    NewsCreationDate = news.NewsCreationDate,
                    IdUser = news.IdUser,
                    IdNewsCategory = news.IdNewsCategory
                };
                _context.News.Add(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", news.IdNewsCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", news.IdUser);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", news.IdNewsCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", news.IdUser);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNews,NewsTitle,NewsDescription,NewsImage,NewsCreationDate,IdUser,IdNewsCategory")] News news)
        {
            if (id != news.IdNews)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.IdNews))
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
            ViewData["IdNewsCategory"] = new SelectList(_context.NewsCategories, "IdNewsCategory", "IdNewsCategory", news.IdNewsCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", news.IdUser);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.IdNewsCategoryNavigation)
                .Include(n => n.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdNews == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'ProyectoContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.IdNews == id)).GetValueOrDefault();
        }
    }
}
