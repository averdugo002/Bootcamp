using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolesDefinitivoBanco.Models
{
    public class Banco
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Sucursal> Sucursales { get; set; }
    }
}
