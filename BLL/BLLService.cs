using System;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class BLLService
    {

        private conexion Cliente = new conexion();
        private conexion Usuario = new conexion();
        private conexion data = new conexion();
        private transportistaDAL transportistaDAL = new transportistaDAL();

        public DataTable GetData()
        {
            return Cliente.GetData();
        }

        public DataTable GetUsuarios() {

            return Usuario.GetUsuarios();
        }

        public DataTable GetTransportista()
        {
            return data.GetTransportista();
        }

        public DataTable GetPedidos()
        {
            return data.GetPedidos();
        }

        public DataTable GetTransportistas()
        {
            return data.GetTransportista();
        }

        public DataTable GetEnvios()
        {
            return data.GetEnvios();
        }
    }
}
