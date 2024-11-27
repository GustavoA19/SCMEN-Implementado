using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class cliente : persona
    {
        public string Direccion {  get; set; }
        public string Entrega { get; set; }

        public cliente() { }

        public cliente(int ID, string Nombre, string Apellido, string Email, string Telefono, string direccion, string entrega)
            : base(ID, Nombre, Apellido, Email, Telefono)
        {
            this.Direccion = direccion;
            this.Entrega = entrega;
        }
    }
}
