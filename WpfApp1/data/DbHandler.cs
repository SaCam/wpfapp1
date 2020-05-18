using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp1.data
{    
    class DbHandler
    {
        ///////////Returns connectionString from App.config by name//////////////////////////////
        private static string CnnVal(string name)
        {
            //grabs info from App.config by name
            return ConfigurationManager.ConnectionStrings[name].ConnectionString; //return string as defined in App.config
        }

        /// <summary>
        /// Rerturns string with relative path from app.config
        /// </summary>
        /// <returns></returns>
        public static string GetConStr()
        {
            ConnectionStringSettings c = ConfigurationManager.ConnectionStrings["localDbConnection"];
            string wd = AppDomain.CurrentDomain.BaseDirectory;
            string fixedConnectionString = c.ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory);
            return fixedConnectionString;
        }


        ////////////////To read data from query//////////////////////////////
        private static String ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}", record[0]);
        }


        ////////////////Method to Query "Team" table in Local database. By use of App.Config and connectionString.///////////////////
        public static List<String> QTable(string col, string table)
        {
            //Variable to store List of teams in.
            List<String> colume = new List<String>();

            //Using closes connection automatically
            using (SqlConnection connection = new SqlConnection(CnnVal("localDbConnection")))
            {
                //Open SQL connection (asynic)
                connection.Open();

                //Create sql command
                SqlCommand command = new SqlCommand(string.Format("SELECT {0} FROM {1}", col, table), connection);

                //execute here
                SqlDataReader dataReader = command.ExecuteReader();
                // Call Read before accessing data.
                while (dataReader.Read())
                {
                    colume.Add(ReadSingleRow((IDataRecord)dataReader));
                }
            }
            //Return list of strings (Name of teams)
            return colume;
        }

        ////////////////Row from Database///////////////////
        public static int Qid(string name)
        {
            int id = new int();
            //Using closes connection automatically
            using (SqlConnection connection = new SqlConnection(CnnVal("localDbConnection")))
            {
                //Open SQL connection (asynic)
                connection.Open();

                //Create sql command
                SqlCommand command = new SqlCommand(string.Format("SELECT id FROM Team WHERE name='{0}'", name), connection);

                //execute here
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            //Return list of strings (Name of teams)
            return id;
        }

        public static void StoreTable(string table, string col, string val)
        {
            using (SqlConnection connection = new SqlConnection(CnnVal("localDbConnection")))
            {
                connection.Open(); //Open connection with sql database
                SqlCommand command = new SqlCommand();
                command.CommandText = string.Format($"INSERT INTO {table} ({col}) VALUES ({val})");
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
        
        ///////////// Function returns a byte array representing an Image.////////////
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
            //Changes an Image to a bytearray
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        ///////////////// Function returns an Image from Byte array. ////////////////
        public static Image ByteArrayToImage(byte[] byteArrayIn)
            //Changes a byte array to a Image
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
