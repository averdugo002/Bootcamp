using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolesDefinitivoBanco.Data;
using RolesDefinitivoBanco.Models;

namespace RolesDefinitivoBanco.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public object ClientScript { get; private set; }

        public ClientesController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        //[TempData]
        //public string StatusMessage { get; set; }
        // GET: Clientes
        public async Task<IActionResult> Index(double balance, string searchString)
        {
            var applicationDbContext = _context.Cliente.Include(c => c.Sucursal);

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter1"] = balance;


            List<Cliente> clientes = await applicationDbContext.ToListAsync(); //Esto nos mostrará todo lo de la BBDD



            if (!String.IsNullOrEmpty(searchString) || balance != 0)
            {
                clientes = await _context.Cliente.Where(s => s.Nombre.Contains(searchString)
                               || s.Apellido.Contains(searchString)).ToListAsync();
                clientes = await _context.Cliente.Where(s => s.Balance == balance).ToListAsync();
            }

            AppUser usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(usuario, "admin");
            }

            return View(clientes);

        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,NúmeroCuenta,Balance,SucursalId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre", cliente.SucursalId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Id", cliente.SucursalId);
            return View(cliente);
        }
        //Get ingresar
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Ingresar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre", cliente.SucursalId);
            return View(cliente);
        }
        // Get devolver
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre", cliente.SucursalId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,NúmeroCuenta,Balance,SucursalId")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre", cliente.SucursalId);
            return View(cliente);
        }
        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(int id, double ingresoDinero)
        {
            Cliente cliente = await _context.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cliente.Balance = cliente.Balance + ingresoDinero;
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Id", cliente.SucursalId);
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id, double devolucionDinero)
        {
            Cliente cliente = await _context.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Esta fórmula lo que hace es que saque un mensaje si intenta retirar más de la cuenta.
                    if (cliente.Balance < devolucionDinero)// Ojo, lo que queremos es que sea siempre menos de lo que tiene.
                    {
                        TempData["msg"] = "<script>alert('No puede retirar más de lo que tiene.');</script>";
                        return View(cliente);
                    }
                    else
                    {

                        cliente.Balance = cliente.Balance - devolucionDinero;
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();
                        //ViewBag.Alert = "Está intentado retirar más dinero del que tiene.";
                        //return View("Index");
                        //return View(script);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre", cliente.SucursalId);
            return View(cliente);
        }


        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
