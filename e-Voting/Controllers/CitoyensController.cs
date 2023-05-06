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
    public class CitoyensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitoyensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citoyens
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Citoyens.Include(c => c.BureauVote);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Citoyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citoyens == null)
            {
                return NotFound();
            }

            var citoyen = await _context.Citoyens
                .Include(c => c.BureauVote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citoyen == null)
            {
                return NotFound();
            }

            return View(citoyen);
        }

        // GET: Citoyens/Create
        public IActionResult Create()
        {
            ViewData["BureauVoteId"] = new SelectList(_context.BureauVotes, "Id", "NomBureauVote");
            return View();
        }

        // POST: Citoyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prénom,Nom,DateNaissance,Adresse,BureauVoteId")] Citoyen citoyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citoyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BureauVoteId"] = new SelectList(_context.BureauVotes, "Id", "NomBureauVote", citoyen.BureauVoteId);
            return View(citoyen);
        }

        // GET: Citoyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citoyens == null)
            {
                return NotFound();
            }

            var citoyen = await _context.Citoyens.FindAsync(id);
            if (citoyen == null)
            {
                return NotFound();
            }
            ViewData["BureauVoteId"] = new SelectList(_context.BureauVotes, "Id", "NomBureauVote", citoyen.BureauVoteId);
            return View(citoyen);
        }

        // POST: Citoyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prénom,Nom,DateNaissance,Adresse,BureauVoteId")] Citoyen citoyen)
        {
            if (id != citoyen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citoyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitoyenExists(citoyen.Id))
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
            ViewData["BureauVoteId"] = new SelectList(_context.BureauVotes, "Id", "Id", citoyen.BureauVoteId);
            return View(citoyen);
        }

        // GET: Citoyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citoyens == null)
            {
                return NotFound();
            }

            var citoyen = await _context.Citoyens
                .Include(c => c.BureauVote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citoyen == null)
            {
                return NotFound();
            }

            return View(citoyen);
        }

        // POST: Citoyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citoyens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Citoyens'  is null.");
            }
            var citoyen = await _context.Citoyens.FindAsync(id);
            if (citoyen != null)
            {
                _context.Citoyens.Remove(citoyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitoyenExists(int id)
        {
          return _context.Citoyens.Any(e => e.Id == id);
        }
    }
}
