using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class usuarioDAL
    {
       
        
            private string conex = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                                   "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";

            public void setUsuario(ENTITY.usuario u)
            {
                using (OracleConnection conn = new OracleConnection(conex))
                {
                    conn.Open();
                    string consulta = "INSERT INTO USUARIO(ID, Nombre, Apellido, Email, Telefono, NombreUsuario, Contraseña, Rol)" +
                                      " VALUES(:ID, :Nombre, :Apellido, :Email, :Telefono, :NombreUsuario, :Contraseña, :Rol)";
                    using (OracleCommand cmd = new OracleCommand(consulta, conn))
                    {
                        cmd.Parameters.Add("ID", u.ID);
                        cmd.Parameters.Add("Nombre", u.Nombre);
                        cmd.Parameters.Add("Apellido", u.Apellido);
                        cmd.Parameters.Add("Email", u.Email);
                        cmd.Parameters.Add("Telefono", u.Telefono);
                        cmd.Parameters.Add("NombreUsuario", u.NombreUsuario);
                        cmd.Parameters.Add("Contraseña", u. Contraseña);
                        cmd.Parameters.Add("Rol", u.Rol);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void deleteUsuario(int usuarioID)
            {
                using (OracleConnection conn = new OracleConnection(conex))
                {
                    conn.Open();
                    string consulta = "DELETE FROM USUARIO WHERE ID = :ID";
                    using (OracleCommand cmd = new OracleCommand(consulta, conn))
                    {
                        cmd.Parameters.Add("ID", usuarioID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public bool updateUsuario(ENTITY.usuario u)
            {
                using (OracleConnection conn = new OracleConnection(conex))
                {
                    conn.Open();
                string consulta = "UPDATE USUARIO SET Nombre = :Nombre, Apellido = :Apellido, Email = :Email, Telefono = :Telefono, NombreUsuario = :NombreUsuario, " +
                    "Contraseña = :Contraseña, Rol = :Rol WHERE ID = :ID";


                using (OracleCommand cmd = new OracleCommand(consulta, conn))
                    {
                        cmd.Parameters.Add("Nombre", u.Nombre);
                        cmd.Parameters.Add("Apellido", u.Apellido);
                        cmd.Parameters.Add("Email", u.Email);
                        cmd.Parameters.Add("Telefono", u.Telefono);
                        cmd.Parameters.Add("NombreUsuario", u.NombreUsuario);
                        cmd.Parameters.Add("Contraseña", u.Contraseña);
                        cmd.Parameters.Add("Rol", u.Rol);
                        cmd.Parameters.Add(new OracleParameter("ID", u.ID));
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }

           public string login(string usuario, string contraseña)
        {
            using (OracleConnection conn = new OracleConnection(conex))
            {
                conn.Open();
                string consulta = "SELECT Rol FROM USUARIO WHERE NombreUsuario = :NombreUsuario AND Contraseña = :Contraseña";
                using (OracleCommand cmd = new OracleCommand(consulta, conn))
                {
                    cmd.Parameters.Add("NombreUsuario", usuario);
                    cmd.Parameters.Add("Contraseña", contraseña);
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Retornar el rol del usuario autenticado.
                        return reader["Rol"].ToString();
                    }
                    else
                    {
                        return null; // Indicar que no se encontró el usuario.
                    }
                }
            }
        }
    }
}
