using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class factura
    {
        public int IdFactura {  get; set; }
        public int IdCliente { get; set; }
        public int IdEnvio { get; set; }
        public double CostoEnvio { get; set; }
        public DateTime FechaEmision {  get; set; }

        public factura() { }
        public factura(int IdFactura, int IdCliente, int IdEnvio, double costoEnvio, DateTime fechaEmision, string metodoPago)
        {
            this.IdFactura = IdFactura;
            this.IdCliente = IdCliente;
            this.IdEnvio = IdEnvio;
            this.CostoEnvio = costoEnvio;
            this.FechaEmision = fechaEmision;
        }
    }
}
