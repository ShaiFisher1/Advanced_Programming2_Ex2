using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Data;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers
{
    public class RankingItemsController : Controller
    {
        private readonly MyWebApplicationContext _context;

        public RankingItemsController(MyWebApplicationContext context)
        {
            _context = context;
        }

        // GET: RankingItems
        public async Task<IActionResult> Index()
        {
              return View(await _context.RankingItem.ToListAsync());
        }

        public async Task<IActionResult> Search()
        {
            return View(await _context.RankingItem.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var q = from   rankingItem in _context.RankingItem
                    where  rankingItem.Feedback.Contains(query)
                    select rankingItem;

            return View(await q.ToListAsync());
        }

        // GET: RankingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RankingItem == null)
            {
                return NotFound();
            }

            var rankingItem = await _context.RankingItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankingItem == null)
            {
                return NotFound();
            }

            return View(rankingItem);
        }

        // GET: RankingItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RankingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ranking,Feedback,Name,Time")] RankingItem rankingItem)
        {
            rankingItem.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            if (ModelState.IsValid)
            {
                _context.Add(rankingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rankingItem);
        }

        // GET: RankingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RankingItem == null)
            {
                return NotFound();
            }

            var rankingItem = await _context.RankingItem.FindAsync(id);
            rankingItem.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            if (rankingItem == null)
            {
                return NotFound();
            }
            return View(rankingItem);
        }

        // POST: RankingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ranking,Feedback,Name,Time")] RankingItem rankingItem)
        {
            if (id != rankingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingItemExists(rankingItem.Id))
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
            return View(rankingItem);
        }

        // GET: RankingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RankingItem == null)
            {
                return NotFound();
            }

            var rankingItem = await _context.RankingItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankingItem == null)
            {
                return NotFound();
            }

            return View(rankingItem);
        }

        // POST: RankingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RankingItem == null)
            {
                return Problem("Entity set 'MyWebApplicationContext.RankingItem'  is null.");
            }
            var rankingItem = await _context.RankingItem.FindAsync(id);
            if (rankingItem != null)
            {
                _context.RankingItem.Remove(rankingItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingItemExists(int id)
        {
          return _context.RankingItem.Any(e => e.Id == id);
        }
    }
}
