using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using ENTITY;
using System.Threading.Tasks;

namespace BLL
{
    public class facturaBLL
    {
        private factura factura = new factura();
        private facturaDAL set = new facturaDAL();
        public bool InsertarFactura(factura f)
        {
            try
            {
                set.InsertarFactura(f);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar factura: {ex.Message}");
            }
        }
    }
}
