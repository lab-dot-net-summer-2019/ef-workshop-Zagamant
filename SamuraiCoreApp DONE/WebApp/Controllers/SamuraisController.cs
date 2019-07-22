using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace WebApp.Controllers
{
    public class SamuraisController : Controller
    {
        private readonly SamuraiContext _context;

        public SamuraisController(SamuraiContext context)
        {
            _context = context;
        }

        // GET: Samurais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Samurais.ToListAsync());
        }

        // GET: Samurais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO
            //Get single Samurai, including quotes and SecretIdentity with id = id (query param)
            var samurai = _context.Samurais
                .Include(s => s.Quotes)
                .Include(s => s.SecretIdentity)
                .FirstOrDefault(s => s.Id == id);

            if (samurai == null)
            {
                return NotFound();
            }

            return View(samurai);
        }

        // GET: Samurais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Samurais/Create
        //http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Samurai samurai)
        {
            if (ModelState.IsValid)
            {
                //TODO
                //Add samurai
                await _context.Samurais.AddAsync(samurai).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(samurai);
        }

        // GET: Samurais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO
            //Get single Samurai with quotes and SecretIdentity with id = id (query param)
            var samurai = await _context.Samurais
                .Include(s => s.Quotes)
                .Include(s => s.SecretIdentity)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);

            if (samurai == null) {
                return NotFound();
            }
            return View(samurai);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Samurai samurai)
        public async Task<IActionResult> Edit(int id, Samurai samurai)
        {
            if (id != samurai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO
                    //Update samurai 
                    var samuraiOld = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
                    var secretIdentity = _context.SecretIdentities.FirstOrDefault(x => x.SamuraiId == samurai.Id);
                    if (secretIdentity == null)
                    {
                        await _context.SecretIdentities.AddAsync(samurai.SecretIdentity).ConfigureAwait(false);
                    }
                    else
                    {
                        secretIdentity.RealName = samurai.SecretIdentity.RealName;
                    }
                    samuraiOld.Name = samurai.Name;
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamuraiExists(samurai.Id))
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
            return View(samurai);
        }

        // GET: Samurais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO
            //Get single Samurai with id = id (query param)
            var samurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);

            if (samurai == null) {
                return NotFound();
            }

            return View(samurai);
        }

        // POST: Samurais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //TODO
            //Get single Samurai with id = id (query param)
            //and remove
            var samurai = _context.Samurais.FirstOrDefault(s => s.Id == id);
            if (samurai != null)
            {
                _context.Samurais.Remove(samurai);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SamuraiExists(int id)
        {
            return _context.Samurais.Any(e => e.Id == id);
        }
    }
}
