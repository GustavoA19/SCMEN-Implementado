using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class usuario : persona
    {
        public string NombreUsuario {  get; set; }
        public string Contraseña { get; set; }
        public string Rol {  get; set; }

        public usuario() { }
        public usuario(int ID, string Nombre, string Apellido, string Email, string Telefono, string nombreUsuario, string Contraseña, string Rol)
            : base (ID, Nombre, Apellido, Email, Telefono)
        {
            this.NombreUsuario = nombreUsuario;
            this.Contraseña = Contraseña;
            this.Rol = Rol;
        }
    }
}
