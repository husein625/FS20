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
    public class TrainingsPresencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingsPresencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingsPresences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainingsPresence.Include(t => t.Player).Include(t => t.Training).Include(t => t.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainingsPresences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsPresence = await _context.TrainingsPresence
                .Include(t => t.Player)
                .Include(t => t.Training)
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TrainingsPresenceID == id);
            if (trainingsPresence == null)
            {
                return NotFound();
            }

            return View(trainingsPresence);
        }

        // GET: TrainingsPresences/Create
        public IActionResult Create()
        {
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name");
            ViewData["TrainingID"] = new SelectList(_context.Training, "TrainingID", "Name");
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            return View();
        }

        // POST: TrainingsPresences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingsPresenceID,Date,IsActive,PlayerID,CoachID,TrainingID")] TrainingsPresence trainingsPresence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingsPresence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", trainingsPresence.PlayerID);
            ViewData["TrainingID"] = new SelectList(_context.Training, "TrainingID", "Name", trainingsPresence.TrainingID);
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", trainingsPresence.CoachID);

            return View(trainingsPresence);
        }

        // GET: TrainingsPresences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsPresence = await _context.TrainingsPresence.FindAsync(id);
            if (trainingsPresence == null)
            {
                return NotFound();
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", trainingsPresence.PlayerID);
            ViewData["TrainingID"] = new SelectList(_context.Training, "TrainingID", "Name", trainingsPresence.TrainingID);
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", trainingsPresence.CoachID);

            return View(trainingsPresence);
        }

        // POST: TrainingsPresences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingsPresenceID,Date,IsActive,PlayerID,CoachID,TrainingID")] TrainingsPresence trainingsPresence)
        {
            if (id != trainingsPresence.TrainingsPresenceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingsPresence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingsPresenceExists(trainingsPresence.TrainingsPresenceID))
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
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", trainingsPresence.PlayerID);
            ViewData["TrainingID"] = new SelectList(_context.Training, "TrainingID", "Name", trainingsPresence.TrainingID);
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", trainingsPresence.CoachID);

            return View(trainingsPresence);
        }

        // GET: TrainingsPresences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsPresence = await _context.TrainingsPresence
                .Include(t => t.Player)
                .Include(t => t.Training)
                .FirstOrDefaultAsync(m => m.TrainingsPresenceID == id);
            if (trainingsPresence == null)
            {
                return NotFound();
            }

            return View(trainingsPresence);
        }

        // POST: TrainingsPresences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingsPresence = await _context.TrainingsPresence.FindAsync(id);
            _context.TrainingsPresence.Remove(trainingsPresence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingsPresenceExists(int id)
        {
            return _context.TrainingsPresence.Any(e => e.TrainingsPresenceID == id);
        }
    }
}
