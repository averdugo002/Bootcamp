using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolesDefinitivoBanco.Models
{
    public class Cliente
    {

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NúmeroCuenta { get; set; }
        public double Balance { get; set; }

        public int SucursalId { get; set; }

        public Sucursal Sucursal { get; set; }
        //[TempData]
        //public string StatusMessage { get; set; }
    }
}
