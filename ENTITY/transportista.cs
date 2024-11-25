using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class transportista : persona
    {
        public bool EstadoVerificado {  get; set; }
        public bool Disponibilidad {  get; set; }
        public DateTime FechaRegistro { get; set; }
        public int EnviosDiario { get; set; }
        public float Sueldo { get; set; }

        public transportista()
        {

        }

        public transportista(int ID, string Nombre, string Apellido, string Email, string Telefono, bool estadoVerificado, bool disponibilidad, DateTime fechaRegistro, DateTime registroDiario, int enviosDiario)
            : base(ID, Nombre, Apellido, Email, Telefono)
        {
            this.EstadoVerificado = estadoVerificado;
            this.Disponibilidad = disponibilidad;
            this.FechaRegistro = fechaRegistro;
            this.EnviosDiario = enviosDiario;
            this.Sueldo = 0;
        }
    }
}
