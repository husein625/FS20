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
    public class MembershipFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MembershipFees
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipFee.ToListAsync());
        }

        // GET: MembershipFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipFee = await _context.MembershipFee
                .FirstOrDefaultAsync(m => m.MembershipFeeID == id);
            if (membershipFee == null)
            {
                return NotFound();
            }

            return View(membershipFee);
        }

        // GET: MembershipFees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipFeeID,Name,amount")] MembershipFee membershipFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipFee);
        }

        // GET: MembershipFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipFee = await _context.MembershipFee.FindAsync(id);
            if (membershipFee == null)
            {
                return NotFound();
            }
            return View(membershipFee);
        }

        // POST: MembershipFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipFeeID,Name,amount")] MembershipFee membershipFee)
        {
            if (id != membershipFee.MembershipFeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipFeeExists(membershipFee.MembershipFeeID))
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
            return View(membershipFee);
        }

        // GET: MembershipFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipFee = await _context.MembershipFee
                .FirstOrDefaultAsync(m => m.MembershipFeeID == id);
            if (membershipFee == null)
            {
                return NotFound();
            }

            return View(membershipFee);
        }

        // POST: MembershipFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipFee = await _context.MembershipFee.FindAsync(id);
            _context.MembershipFee.Remove(membershipFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipFeeExists(int id)
        {
            return _context.MembershipFee.Any(e => e.MembershipFeeID == id);
        }
    }
}
