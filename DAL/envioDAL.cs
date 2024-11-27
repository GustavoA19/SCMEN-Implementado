using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class envioDAL
    {
        private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";

        public void setEnvio(ENTITY.envio e)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Inicia la transacción
                {
                    try
                    {
                        string consulta = "INSERT INTO ENVIO(ID_ENVIO, ID_CLIENTE, ID_PAQUETE, DIRECCIONENVIO, DIRECCIONENTREGA)" +
                                          " VALUES(:ID_ENVIO, :ID_CLIENTE, :ID_PAQUETE, :DIRECCIONENVIO, :DIRECCIONENTREGA)";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Parameters.Add("DIRECCIONENTREGA", e.DireccionEntrega);
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

        public void asignarTransportista(ENTITY.transportista t, ENTITY.envio e)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Inicia la transacción
                {
                    try
                    {
                        string consulta = "UPDATE ENVIO SET ID_TRANSPORTISTA = :ID_TRANSPORTISTA WHERE ID_ENVIO = :ID_ENVIO";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Parameters.Add("ID_TRANSPORTISTA", t.ID);
                            cmd.Parameters.Add("ID_ENVIO", e.ID);
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
