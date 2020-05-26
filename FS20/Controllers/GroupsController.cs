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
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Coach")]
        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Group.Include(a => a.Employee).Include(e => e.Employee1).Include(d => d.Employee2);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .Include(a => a.Employee)
                .Include(e => e.Employee1)
                .Include(d => d.Employee2)
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            ViewData["AssistantCoach1ID"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            ViewData["AssistantCoach2ID"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupID,Name,Age,NumberOfPlayers,DateFrom,DateTo,CoachID,AssistantCoach1ID,AssistantCoach2ID")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.CoachID);
            ViewData["AssistantCoach1ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach1ID);
            ViewData["AssistantCoach2ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach2ID);
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.CoachID);
            ViewData["AssistantCoach1ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach1ID);
            ViewData["AssistantCoach2ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach2ID);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupID,Name,Age,NumberOfPlayers,DateFrom,DateTo,CoachID,AssistantCoach1ID,AssistantCoach2ID")] Group @group)
        {
            if (id != @group.GroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupID))
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
            ViewData["CoachID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.CoachID);
            ViewData["AssistantCoach1ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach1ID);
            ViewData["AssistantCoach2ID"] = new SelectList(_context.Employee, "EmployeeId", "Name", @group.AssistantCoach2ID);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .Include(a => a.Employee)
                .Include(e => e.Employee1)
                .Include(d => d.Employee2)
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Group.FindAsync(id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.GroupID == id);
        }
    }
}
