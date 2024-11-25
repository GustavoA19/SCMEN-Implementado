using DAL;
using System;
using ENTITY;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class envioBLL
    {
        private envioDAL envioDAL = new envioDAL();
        public void asignarTransportista(transportista t, envio e)
        {
            try
            {
                envioDAL.asignarTransportista(t, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
