using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class paqueteBLL
    {
        private paqueteDAL set = new paqueteDAL();
        public void setPaquete(ENTITY.paquete p)
        {
            try
            {
                set.setPaquete(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
