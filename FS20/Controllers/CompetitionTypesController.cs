using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS20.Data;
using FS20.Models;

namespace FS20.Controllers
{
    public class CompetitionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompetitionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompetitionType.ToListAsync());
        }

        // GET: CompetitionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionType = await _context.CompetitionType
                .FirstOrDefaultAsync(m => m.CompetitionTypeID == id);
            if (competitionType == null)
            {
                return NotFound();
            }

            return View(competitionType);
        }

        // GET: CompetitionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompetitionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionTypeID,Name")] CompetitionType competitionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competitionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competitionType);
        }

        // GET: CompetitionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionType = await _context.CompetitionType.FindAsync(id);
            if (competitionType == null)
            {
                return NotFound();
            }
            return View(competitionType);
        }

        // POST: CompetitionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionTypeID,Name")] CompetitionType competitionType)
        {
            if (id != competitionType.CompetitionTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competitionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionTypeExists(competitionType.CompetitionTypeID))
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
            return View(competitionType);
        }

        // GET: CompetitionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionType = await _context.CompetitionType
                .FirstOrDefaultAsync(m => m.CompetitionTypeID == id);
            if (competitionType == null)
            {
                return NotFound();
            }

            return View(competitionType);
        }

        // POST: CompetitionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competitionType = await _context.CompetitionType.FindAsync(id);
            _context.CompetitionType.Remove(competitionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionTypeExists(int id)
        {
            return _context.CompetitionType.Any(e => e.CompetitionTypeID == id);
        }
    }
}
