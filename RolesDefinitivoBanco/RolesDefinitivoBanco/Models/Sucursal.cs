using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RolesDefinitivoBanco.Models
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int BancoId { get; set; }
        public List<Cliente> Clientes { get; set; }
        public Banco Banco { get; set; }
    }
}
