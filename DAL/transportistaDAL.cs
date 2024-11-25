
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class transportistaDAL
    {
        private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";

        
        public void setTransportista(ENTITY.transportista t)
        {
            {
                using (OracleConnection connection = new OracleConnection(conex))
                {
                    string query = @"
                INSERT INTO TRANSPORTISTA (
                    ID, NOMBRE, APELLIDO, EMAIL, TELEFONO, DISPONIBILIDAD, 
                    FECHAREGISTRO, ENVIOSDIARIO, SUELDO
                ) VALUES (
                    :ID, :NOMBRE, :APELLIDO, :EMAIL, :TELEFONO, :DISPONIBILIDAD, 
                    :FECHAREGISTRO, :ENVIOSDIARIO, :SUELDO
                )";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("ID", t.ID));
                        command.Parameters.Add(new OracleParameter("NOMBRE", t.Nombre));
                        command.Parameters.Add(new OracleParameter("APELLIDO", t.Apellido));
                        command.Parameters.Add(new OracleParameter("EMAIL", t.Email));
                        command.Parameters.Add(new OracleParameter("TELEFONO", t.Telefono));
                        command.Parameters.Add(new OracleParameter("DISPONIBILIDAD", OracleDbType.Int32)).Value = t.Disponibilidad ? 1 : 0;
                        command.Parameters.Add(new OracleParameter("FECHAREGISTRO", t.FechaRegistro));
                        command.Parameters.Add(new OracleParameter("ENVIOSDIARIO", t.EnviosDiario));
                        command.Parameters.Add(new OracleParameter("SUELDO", t.Sueldo));

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        public void deleteTransportista(ENTITY.transportista t)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaccion = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "DELETE FROM TRANSPORTISTA WHERE ID = :ID";
                        using (OracleCommand cmd = new OracleCommand(query, conn))
                        {
                            cmd.Parameters.Add(new OracleParameter("ID", t.ID));
                            cmd.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        throw new Exception($"Error al eliminar transportista: {ex.Message}");
                    }
                }
            }
        }

        public void updateTransportista(ENTITY.transportista t)
            {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaccion = conn.BeginTransaction())
                {
                    try
                    {
                       string query = @"
                            UPDATE transportista
                            SET
                                NOMBRE = :Nombre,
                                APELLIDO = :Apellido,
                                EMAIL = :Email,
                                TELEFONO = :Telefono,
                                DISPONIBILIDAD = :Disponibilidad,
                                FECHAREGISTRO = :FechaRegistro,
                                ENVIOSDIARIO = :EnviosDiario,
                                SUELDO = :Sueldo
                            WHERE
                                ID = :ID";

                            using (OracleCommand command = new OracleCommand(query, conn))
                            {
                                // Asignar los valores de los parámetros
                                command.Parameters.Add(new OracleParameter("Nombre", t.Nombre));
                                command.Parameters.Add(new OracleParameter("Apellido", t.Apellido));
                                command.Parameters.Add(new OracleParameter("Email", t.Email));
                                command.Parameters.Add(new OracleParameter("Telefono", t.Telefono));
                                command.Parameters.Add(new OracleParameter("Disponibilidad", OracleDbType.Int32)).Value = t.Disponibilidad ? 1 : 0;
                            command.Parameters.Add(new OracleParameter("FechaRegistro", t.FechaRegistro));
                                command.Parameters.Add(new OracleParameter("EnviosDiario", t.EnviosDiario));
                                command.Parameters.Add(new OracleParameter("Sueldo", t.Sueldo));
                                command.Parameters.Add(new OracleParameter("ID", t.ID)); // Clave primaria
                                int rowsAffected = command.ExecuteNonQuery();
                                transaccion.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw new Exception($"Error al actualizar transportista: {ex.Message}");
                        }
                }
            }
        }
    }
}
