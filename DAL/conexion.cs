using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class conexion
    {
        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));User Id=ADMINISTRADOR;Password=2005;";
        public static conexion con = null;
        public conexion()
        {
            //Constructor
        }
        public DataTable GetPedidos()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                //SELECT E.ID_Envio, E.DireccionEnvio, E.DireccionEntrega FROM ENVIO E
                OracleCommand cmd = new OracleCommand(@"SELECT E.ID_ENVIO, 
                                                       C.ID AS ID_CLIENTE, 
                                                       C.NOMBRE AS NOMBRE_CLIENTE,
                                                       C.APELLIDO AS APELLIDO_CLIENTE,
                                                       C.EMAIL AS EMAIL_CLIENTE,
                                                       C.TELEFONO AS TELEFONO_CLIENTE,                 
                                                       E.DIRECCIONENVIO AS DIRECCIONENVIO, 
                                                       E.DIRECCIONENTREGA AS DIRECCIONENTREGA,
                                                       P.DESCRIPCION AS DESCRIPCION_PAQUETE
                                                       FROM ENVIO E
                                                       JOIN CLIENTE C ON E.ID_CLIENTE = C.ID
                                                       JOIN PAQUETE P ON E.ID_PAQUETE = P.ID_PAQUETE", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetData()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM CLIENTE", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }

        }
        public DataTable GetUsuarios()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM USUARIO", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetClients()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM CLIENTE", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }

        }
        public DataTable GetTransportista()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM TRANSPORTISTA", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        protected OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }

        public DataTable GetEnvios()
        {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    //SELECT E.ID_Envio, E.DireccionEnvio, E.DireccionEntrega FROM ENVIO E
                    OracleCommand cmd = new OracleCommand(@"SELECT E.ID_ENVIO, 
                                                       C.ID AS ID_CLIENTE, 
                                                       C.NOMBRE AS NOMBRE_CLIENTE,
                                                       E.DIRECCIONENVIO AS DIRECCIONENVIO, 
                                                       E.DIRECCIONENTREGA AS DIRECCIONENTREGA
                                                       FROM ENVIO E
                                                       JOIN CLIENTE C ON E.ID_CLIENTE = C.ID", conn);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
        }

        public DataTable getTransportista()
        {
            DataTable data = new DataTable();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID, NOMBRE FROM TRANSPORTISTA";
                    OracleDataAdapter da = new OracleDataAdapter(query, conn);
                    da.Fill(data);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine(ex.Message);
                }
            }
            return data;
        }
    }
}
