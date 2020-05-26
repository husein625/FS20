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
    public class EmployeePositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeePositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: EmployeePositions
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeePosition.ToListAsync());
        }

        // GET: EmployeePositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = await _context.EmployeePosition
                .FirstOrDefaultAsync(m => m.EmployeePositionId == id);
            if (employeePosition == null)
            {
                return NotFound();
            }

            return View(employeePosition);
        }

        // GET: EmployeePositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeePositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeePositionId,Name")] EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeePosition);
        }

        // GET: EmployeePositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = await _context.EmployeePosition.FindAsync(id);
            if (employeePosition == null)
            {
                return NotFound();
            }
            return View(employeePosition);
        }

        // POST: EmployeePositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeePositionId,Name")] EmployeePosition employeePosition)
        {
            if (id != employeePosition.EmployeePositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePositionExists(employeePosition.EmployeePositionId))
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
            return View(employeePosition);
        }

        // GET: EmployeePositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = await _context.EmployeePosition
                .FirstOrDefaultAsync(m => m.EmployeePositionId == id);
            if (employeePosition == null)
            {
                return NotFound();
            }

            return View(employeePosition);
        }

        // POST: EmployeePositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePosition = await _context.EmployeePosition.FindAsync(id);
            _context.EmployeePosition.Remove(employeePosition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePositionExists(int id)
        {
            return _context.EmployeePosition.Any(e => e.EmployeePositionId == id);
        }
    }
}
