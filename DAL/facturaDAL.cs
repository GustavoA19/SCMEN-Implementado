using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class facturaDAL
    {
        private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";

        public bool InsertarFactura(ENTITY.factura f)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Inicia la transacción
                {
                    try
                    {
                        string consulta = "INSERT INTO FACTURA (ID_Envio, ID_Cliente, FechaEmision, MetodoPago, CostoEnvio) " +
                                                          "VALUES (:ID_Envio, :ID_Cliente, SYSDATE, 'Efectivo', 150.75)";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Parameters.Add("ID_Envio", f.IdEnvio);
                            cmd.Parameters.Add("ID_CLIENTE", f.IdCliente);
                            cmd.ExecuteNonQuery();
                        }


                        transaction.Commit(); // Confirmación de los cambios
                        return true;

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
