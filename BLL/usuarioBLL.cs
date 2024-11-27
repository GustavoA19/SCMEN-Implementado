using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class usuarioBLL
    {
        private usuarioDAL set = new usuarioDAL();

        public void setUsuario(usuario u)
        {
            try
            {
                set.setUsuario(u);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar cliente: {ex.Message}");
            }
        }

        public bool deleteUsuario(int id)
        {
            try
            {
                set.deleteUsuario(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar cliente: {ex.Message}");
            }
        }

        public bool updateUsuario(usuario u)
        {
            usuarioDAL usuarioDAL = new usuarioDAL();
            return usuarioDAL.updateUsuario(u);

        }

        public string login(string user, string pass)
        {
            usuarioDAL usuarioDAL = new usuarioDAL();
            return usuarioDAL.login(user, pass);
        }
    }
}
