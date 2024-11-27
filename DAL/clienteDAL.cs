using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clienteDAL
    {
        private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";
        public void setCliente(ENTITY.cliente c)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Inicia la transacción
                {
                    try
                    {
                        string consulta = "INSERT INTO CLIENTE(ID, Nombre, Apellido, Email, Telefono, Direccion, DireccionEntrega)" +
                                          " VALUES(:ID, :Nombre, :Apellido, :Email, :Telefono, :Direccion, :DireccionEntrega)";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Parameters.Add("ID", c.ID);
                            cmd.Parameters.Add("Nombre", c.Nombre);
                            cmd.Parameters.Add("Apellido", c.Apellido);
                            cmd.Parameters.Add("Email", c.Email);
                            cmd.Parameters.Add("Telefono", c.Telefono);
                            cmd.Parameters.Add("Direccion", c.Direccion);
                            cmd.Parameters.Add("DireccionEntrega", c.Entrega);
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

        public void deleteCliente(int clienteID)
{
    using (OracleConnection conn = new OracleConnection(conex))
    {
        conn.Open();
        using (OracleTransaction transaction = conn.BeginTransaction()) // Iniciar la transacción
        {
            try
            {
                string consulta = "DELETE FROM CLIENTE WHERE ID = :ID";
                using (OracleCommand cmd = new OracleCommand(consulta, conn))
                {
                    cmd.Parameters.Add("ID", clienteID);
                    cmd.ExecuteNonQuery(); // Ejecuta el comando de eliminar
                }

                transaction.Commit(); // Confirmar los cambios si no hubo errores
            }
            catch (Exception)
            {
                transaction.Rollback(); // Revertir los cambios si hubo un error
                throw;
            }
        }
    }
}


        public bool updateCliente(ENTITY.cliente c)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction()) // Iniciar la transacción
                {
                    try
                    {
                        string consulta = "UPDATE CLIENTE SET Nombre = :Nombre, Apellido = :Apellido, Email = :Email, Telefono = :Telefono, Direccion = :Direccion WHERE ID = :ID";
                        using (OracleCommand cmd = new OracleCommand(consulta, conn))
                        {
                            cmd.Transaction = transaction; // Asociar la transacción al comando
                            cmd.Parameters.Add("Nombre", c.Nombre);
                            cmd.Parameters.Add("Apellido", c.Apellido);
                            cmd.Parameters.Add("Email", c.Email);
                            cmd.Parameters.Add("Telefono", c.Telefono);
                            cmd.Parameters.Add("Direccion", c.Direccion);
                            cmd.Parameters.Add(new OracleParameter("ID", c.ID));
                            cmd.ExecuteNonQuery(); // Ejecutar el comando de actualización
                        }

                        transaction.Commit(); // Confirmar la transacción si no hubo errores
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Revertir la transacción si hubo un error
                        throw;
                    }
                }
            }
        }

    }
}
