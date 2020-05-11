using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, col, val);
                command.Connection = connection;
                command.ExecuteNonQuery();

            }
        }
    }
}
