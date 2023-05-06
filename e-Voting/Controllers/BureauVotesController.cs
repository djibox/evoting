using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Voting.Data;
using e_Voting.Models.Domain;

namespace e_Voting.Controllers
{
    public class BureauVotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BureauVotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BureauVotes
        public async Task<IActionResult> Index()
        {
              return _context.BureauVotes != null ? 
                          View(await _context.BureauVotes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BureauVotes'  is null.");
        }

        // GET: BureauVotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BureauVotes == null)
            {
                return NotFound();
            }

            var bureauVote = await _context.BureauVotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bureauVote == null)
            {
                return NotFound();
            }

            return View(bureauVote);
        }

        // GET: BureauVotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BureauVotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomBureauVote,NombreInscrits")] BureauVote bureauVote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bureauVote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bureauVote);
        }

        // GET: BureauVotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BureauVotes == null)
            {
                return NotFound();
            }

            var bureauVote = await _context.BureauVotes.FindAsync(id);
            if (bureauVote == null)
            {
                return NotFound();
            }
            return View(bureauVote);
        }

        // POST: BureauVotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomBureauVote,NombreInscrits")] BureauVote bureauVote)
        {
            if (id != bureauVote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bureauVote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BureauVoteExists(bureauVote.Id))
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
            return View(bureauVote);
        }

        // GET: BureauVotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BureauVotes == null)
            {
                return NotFound();
            }

            var bureauVote = await _context.BureauVotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bureauVote == null)
            {
                return NotFound();
            }

            return View(bureauVote);
        }

        // POST: BureauVotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BureauVotes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BureauVotes'  is null.");
            }
            var bureauVote = await _context.BureauVotes.FindAsync(id);
            if (bureauVote != null)
            {
                _context.BureauVotes.Remove(bureauVote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BureauVoteExists(int id)
        {
          return (_context.BureauVotes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
