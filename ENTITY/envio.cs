using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class envio
    {
        public int ID {  get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaEntrega { get; set;}
        public string EstadoEnvio { get; set; }
        public envio () { }
        public envio (int iD, string direccionEntrega, DateTime fechaEnvio, DateTime fechaEntrega, string estadoEnvio)
        {
            this.ID = iD;
            this.DireccionEntrega = direccionEntrega;
            this.FechaEnvio = fechaEnvio;
            this.FechaEntrega = fechaEntrega;
            this.EstadoEnvio = estadoEnvio;
        }
    }
}
