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
    public class PlayerMembershipFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerMembershipFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: PlayerMembershipFees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlayerMembershipFee.Include(p => p.MembershipFee).Include(p => p.Player);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlayerMembershipFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerMembershipFee = await _context.PlayerMembershipFee
                .Include(p => p.MembershipFee)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerMembershipFeeID == id);
            if (playerMembershipFee == null)
            {
                return NotFound();
            }

            return View(playerMembershipFee);
        }

        [Authorize(Roles = "Economist")]
        // GET: PlayerMembershipFees/Create
        public IActionResult Create()
        {
            ViewData["MembershipFeeID"] = new SelectList(_context.MembershipFee, "MembershipFeeID", "Name");
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name","LastName");
            return View();
        }

        [Authorize(Roles = "Economist")]
        // POST: PlayerMembershipFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerMembershipFeeID,Year,Month,Amount,Date,PlayerID,MembershipFeeID")] PlayerMembershipFee playerMembershipFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerMembershipFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipFeeID"] = new SelectList(_context.MembershipFee, "MembershipFeeID", "Name", playerMembershipFee.MembershipFeeID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", playerMembershipFee.PlayerID);
            return View(playerMembershipFee);
        }

        [Authorize(Roles = "Economist")]
        // GET: PlayerMembershipFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerMembershipFee = await _context.PlayerMembershipFee.FindAsync(id);
            if (playerMembershipFee == null)
            {
                return NotFound();
            }
            ViewData["MembershipFeeID"] = new SelectList(_context.MembershipFee, "MembershipFeeID", "Name", playerMembershipFee.MembershipFeeID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", playerMembershipFee.PlayerID);
            return View(playerMembershipFee);
        }

        [Authorize(Roles = "Economist")]
        // POST: PlayerMembershipFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerMembershipFeeID,Year,Month,Amount,Date,PlayerID,MembershipFeeID")] PlayerMembershipFee playerMembershipFee)
        {
            if (id != playerMembershipFee.PlayerMembershipFeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerMembershipFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerMembershipFeeExists(playerMembershipFee.PlayerMembershipFeeID))
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
            ViewData["MembershipFeeID"] = new SelectList(_context.MembershipFee, "MembershipFeeID", "MembershipFeeID", playerMembershipFee.MembershipFeeID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerId", "Name", playerMembershipFee.PlayerID);
            return View(playerMembershipFee);
        }

        [Authorize(Roles = "Economist")]
        // GET: PlayerMembershipFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerMembershipFee = await _context.PlayerMembershipFee
                .Include(p => p.MembershipFee)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerMembershipFeeID == id);
            if (playerMembershipFee == null)
            {
                return NotFound();
            }

            return View(playerMembershipFee);
        }

        [Authorize(Roles = "Economist")]
        // POST: PlayerMembershipFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerMembershipFee = await _context.PlayerMembershipFee.FindAsync(id);
            _context.PlayerMembershipFee.Remove(playerMembershipFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerMembershipFeeExists(int id)
        {
            return _context.PlayerMembershipFee.Any(e => e.PlayerMembershipFeeID == id);
        }
    }
}
