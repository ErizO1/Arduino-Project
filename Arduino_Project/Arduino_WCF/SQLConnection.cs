using System.Drawing;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

// xY98LBJfs4bLYGDG3

namespace AutoConsultorio
{
    public sealed class SQLConnection
    {
        private static volatile SQLConnection instance; // Instancia única

        private static MySqlConnection conn; // Conexión

        private static MySqlCommand comm; // Comando

        private static MySqlDataReader result; // Resultado de algún query

        private static int rowsAffected; // Guarda la cantidad de registros afectados

        private static List<string[]> results; // Guarda los resultados de la consulta en una matriz de cadenas

        private static bool isConnected; // Propiedad que define si hay una conexión activa

        public static bool init()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "Prueba";
                builder.Password = "Prueba";
                builder.Database = "prueba";
                conn = new MySqlConnection(builder.ToString());
                comm = conn.CreateCommand();
                return true;
            }
            catch
            {
                return false;
            }
        } // Inicializa el objeto

        public static byte connect()
        {
            try
            {
                conn.Open();
                isConnected = true; // Conectado
                return 0; // Todo bien
            }
            catch
            {
                if (isConnected) return 1; // Ya había una conexión
                else return 2; // Error desconocido
            }
        } // Trata de conectar con la base de datos

        public static byte disconnect() 
        {
            try
            {
                conn.Close();
                isConnected = false; // Desconectado
                return 0; // Sin problemas
            }
            catch
            {
                if (!isConnected) return 1; // No hay nada que desconectar
                else return 2; // Error desconocido
            }
        } // Trata de desconectar la base de datos

        public static bool sendQuery(string query, bool needResult)
        {
            results.Clear();
            comm.CommandText = query;
            if (connect() == 2) return false;
            if (needResult)
            {
                result = comm.ExecuteReader();
                results.Add(new string[result.FieldCount]);
                int i;
                for(i=0; i < result.FieldCount; i++) results[0][i] = result.GetName(i);
                i = 1;
                while (result.Read())
                {
                    results.Add(new string[result.FieldCount]);
                    for (int j = 0; j < result.FieldCount; j++) results[i][j] = result[j].ToString();
                    i++;
                }
            rowsAffected = i - 1;
            }
            else rowsAffected = comm.ExecuteNonQuery();
            disconnect();
            return true;

        } // Envía la consulta

        public static string getData(int row, string column)
        {
            try
            {
                int col = 0;
                while (results[0][col] != column) col++;
                return results[row][col];
            }
            catch
            {
                return null;
            }
        } // Obtiene el dato de la columna ingresada en cadena

        public static string getData(int row, int column)
        {
            try
            {
                return results[row][column];
            }
            catch
            {
                return null;
            }
        } // Obtiene el dato de la columna ingresada en número

        public static bool insertImage(string table, string column, int ID, Image img)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                byte[] arr = ms.ToArray();
                if (arr.Length >= (System.Math.Pow(2, 24))) return false;
                string query = "UPDATE " + table + " SET " + column + " = @foto";
                comm.CommandText = query;
                comm.Parameters.AddWithValue("@foto", arr);
                if (connect() == 2) return false;
                comm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        } // Guarda una imagen en un registro existente

        public static Image getImage(string table, string column, int ID)
        {
            try
            {
                if (connect() == 2) return null;
                string query = "SELECT " + column + " FROM " + table + " WHERE ID=" + ID.ToString();
                comm.CommandText = query;
                comm.ExecuteScalar();
                byte[] imgArr;
                imgArr = (byte[])comm.ExecuteScalar();
                var ms = new System.IO.MemoryStream(imgArr);
                Image ret = Image.FromStream(ms);
                disconnect();
                return ret;
            }
            catch
            {
                disconnect();
                return null;
            }
        } // Obtiene una imagen de un registro y un campo específicos

        public static int getRowsAffected()
        {
            return rowsAffected;
        } // Obtiene la cantidad de líneas afectadas por el query

        private SQLConnection()
        {
            results = new List<string[]>();
            init();
        } // Constructor

        public static SQLConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLConnection();
                }
                return instance;
            }
        }
    }
}
