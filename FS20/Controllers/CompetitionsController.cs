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
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Competition.Include(c => c.CompetitionType).Include(c => c.Season);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.CompetitionType)
                .Include(c => c.Season)
                .FirstOrDefaultAsync(m => m.CompetitionID == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            ViewData["CompetitionTypeID"] = new SelectList(_context.CompetitionType, "CompetitionTypeID", "Name");
            ViewData["SeasonID"] = new SelectList(_context.Season, "SeasonID", "Name");
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionID,Name,IsActive,CompetitionTypeID,SeasonID")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionTypeID"] = new SelectList(_context.CompetitionType, "CompetitionTypeID", "Name", competition.CompetitionTypeID);
            ViewData["SeasonID"] = new SelectList(_context.Season, "SeasonID", "Name", competition.SeasonID);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["CompetitionTypeID"] = new SelectList(_context.CompetitionType, "CompetitionTypeID", "Name", competition.CompetitionTypeID);
            ViewData["SeasonID"] = new SelectList(_context.Season, "SeasonID", "Name", competition.SeasonID);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionID,Name,IsActive,CompetitionTypeID,SeasonID")] Competition competition)
        {
            if (id != competition.CompetitionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.CompetitionID))
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
            ViewData["CompetitionTypeID"] = new SelectList(_context.CompetitionType, "CompetitionTypeID", "Name", competition.CompetitionTypeID);
            ViewData["SeasonID"] = new SelectList(_context.Season, "SeasonID", "Name", competition.SeasonID);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.CompetitionType)
                .Include(c => c.Season)
                .FirstOrDefaultAsync(m => m.CompetitionID == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competition = await _context.Competition.FindAsync(id);
            _context.Competition.Remove(competition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competition.Any(e => e.CompetitionID == id);
        }
    }
}
