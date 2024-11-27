using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class paqueteDAL
    {
        private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";
        public void setPaquete(ENTITY.paquete p)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Inicia la transacción
                {
                    try
                    {
                        string consulta = "INSERT INTO PAQUETE( DESCRIPCION, FRAGIL)" +
                                          " VALUES(:DESCRIPCION, :FRAGIL)";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Parameters.Add("DESCRIPCION", p.Descripcion);
                            cmd.Parameters.Add(new OracleParameter("FRAGIL", OracleDbType.Int32)).Value = p.Fragil ? 1 : 0;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Confirmación de los cambios

                    }
                    catch
                    {
                        transaction.Rollback(); // Revierte los cambios en caso de error
                        throw;
                    }
                }
            }
        }
    }
}
