using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ENTITY;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class transportistaBLL
    {
        private transportistaDAL data = new transportistaDAL();
        public void setTransportista(transportista t)
        {
            try
            {
                data.setTransportista(t);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ingresar transportista: {ex.Message}");
            }
        }

        public bool deleteTransportista(transportista t)
        {
            try
            {
                data.deleteTransportista(t);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar transportista: {ex.Message}");
            }
        }

        public bool updateTransportista(transportista t)
        {
            try
            {
                data.updateTransportista(t);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar transportista: {ex.Message}");
            }
        }
    }
}
