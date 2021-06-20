using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RolesDefinitivoBanco.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RolesDefinitivoBanco.Data
{
    public class ApplicationDbContext : IdentityDbContext <AppUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RolesDefinitivoBanco.Models.Sucursal> Sucursal { get; set; }
        public DbSet<RolesDefinitivoBanco.Models.Banco> Banco { get; set; }
        public DbSet<RolesDefinitivoBanco.Models.Cliente> Cliente { get; set; }
    }
}
