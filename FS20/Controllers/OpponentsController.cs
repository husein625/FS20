using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS20.Data;
using FS20.Models;
using Microsoft.AspNetCore.Authorization;

namespace FS20.Controllers
{
    public class OpponentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Coach")]
        // GET: Opponents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opponent.ToListAsync());
        }

        // GET: Opponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opponent = await _context.Opponent
                .FirstOrDefaultAsync(m => m.OpponentID == id);
            if (opponent == null)
            {
                return NotFound();
            }

            return View(opponent);
        }

        // GET: Opponents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpponentID,Name,IsActive")] Opponent opponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opponent);
        }

        // GET: Opponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opponent = await _context.Opponent.FindAsync(id);
            if (opponent == null)
            {
                return NotFound();
            }
            return View(opponent);
        }

        // POST: Opponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpponentID,Name,IsActive")] Opponent opponent)
        {
            if (id != opponent.OpponentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpponentExists(opponent.OpponentID))
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
            return View(opponent);
        }

        // GET: Opponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opponent = await _context.Opponent
                .FirstOrDefaultAsync(m => m.OpponentID == id);
            if (opponent == null)
            {
                return NotFound();
            }

            return View(opponent);
        }

        // POST: Opponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opponent = await _context.Opponent.FindAsync(id);
            _context.Opponent.Remove(opponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpponentExists(int id)
        {
            return _context.Opponent.Any(e => e.OpponentID == id);
        }
    }
}
