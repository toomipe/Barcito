using barcito.Persistencia;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcito.Persistencia
{
    internal class ConexiónMySQL : Conexión
    {
        private MySqlConnection connection;
        private string cadenaconnection;
        public ConexiónMySQL()
        {
        cadenaconnection = "Database=" + database +
               "; DataSource=" + server +
               "; User Id=" + user +
               "; password=" + password +
               "; Connection Timeout=60" +           // 60 segundos para conectarse
               "; DefaultCommandTimeout=60" +        // 60 segundos para comandos (opcional)
               "; Max Pool Size=200" +                // aumentar tamaño máximo del pool
               "; Pooling=true";

        connection = new MySqlConnection(cadenaconnection);
        }

        public MySqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
