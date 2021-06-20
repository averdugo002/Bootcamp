using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RolesDefinitivoBanco.Data;
using RolesDefinitivoBanco.Models;

namespace RolesDefinitivoBanco.Controllers
{
    [Authorize]
    public class BancosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BancosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bancos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Banco.Include(s => s.Sucursales);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bancos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banco == null)
            {
                return NotFound();
            }

            return View(banco);
        }

        // GET: Bancos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bancos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion")] Banco banco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banco);
        }

        // GET: Bancos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }
            return View(banco);
        }

        // POST: Bancos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion")] Banco banco)
        {
            if (id != banco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BancoExists(banco.Id))
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
            return View(banco);
        }

        // GET: Bancos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banco == null)
            {
                return NotFound();
            }

            return View(banco);
        }

        // POST: Bancos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banco = await _context.Banco.FindAsync(id);
            _context.Banco.Remove(banco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BancoExists(int id)
        {
            return _context.Banco.Any(e => e.Id == id);
        }
    }
}
