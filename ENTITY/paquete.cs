using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class paquete
    {
        public string Descripcion { get; set; }
        public bool Fragil {  get; set; }

        public paquete() { }

        public paquete (int iD, string descripcion, double dimension, double peso, bool fragil, string estadoPaquete)
        {
            this.Descripcion = descripcion;
            this.Fragil = fragil;
        }
    }
}
