using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using ENTITY;
using System.Threading.Tasks;

namespace BLL
{
    public class clienteBLL
    {
        private clienteDAL set = new clienteDAL();

        public void setCliente(cliente c)
        {
            try
            {
                set.setCliente(c);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar cliente: {ex.Message}");
            }
        }

        public bool deleteCliente(int id)
        {
            try
            {
                set.deleteCliente(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar cliente: {ex.Message}");
            }
        }

        public bool updateCliente(cliente c)
        {
              clienteDAL clienteDAL = new clienteDAL();
            return clienteDAL.updateCliente(c); 

        }
    }
}
