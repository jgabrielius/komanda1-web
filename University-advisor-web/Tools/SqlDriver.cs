using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Data.SqlClient;
using System.Data;
using University_advisor_web.EntityFramework;

namespace University_advisor_web
{
    public class SqlDriver
    {
        private static SQLiteConnection Connect()
        {
            try
            {
                var dbConnection = new SQLiteConnection("Data Source=Database.sqlite;Version=3;foreign keys=true;");
                dbConnection.Open();
                return dbConnection;
            }
            catch (SQLiteException e)
            {
                //TODO add logging
                return null;
            }
        }

        public static List<Dictionary<string,object>> Fetch(string sql)
        {
            var dbConnection = Connect();

            if (dbConnection == null)
            {
                return null;
            }
            var list = new List<Dictionary<string,object>>();
            try
            {
                var command = new SQLiteCommand(sql, dbConnection);
                var reader = command.ExecuteReader();
                command.Dispose();

                while (reader.Read())
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dictionary[reader.GetName(i)] = reader.GetValue(i);
                    }
                    list.Add(dictionary);
                }

                reader.Close();
                dbConnection.Close();
                return list;
            }
            catch (SQLiteException e)
            {
               
                return null;
            }
        }

        public static Dictionary<string,object> Row (string sql)
        {
            List<Dictionary<string, object>> result = Fetch(sql);

            if(result != null && result.Count !=0)
            {
                return result[0];
            } else
            {
                return null;
            }
            
        }

        public static bool Exists(string sql)
        {
            var result = Fetch(sql);
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static SQLiteCommand Replace(SQLiteConnection dbConnection, string sql, ArrayList parameters)
        {
            var command = new SQLiteCommand(sql, dbConnection);
            for(int i = 0; i < parameters.Count; i++)
            {
                SQLiteParameter param = new SQLiteParameter()
                {
                    ParameterName = "@"+i,
                    Value = parameters[i]
                };
                command.Parameters.Add(param);
            }
            return command;
        }

        public static bool Execute(string sql, ArrayList parameters = null)
        {
            var dbConnection = Connect();

            if (dbConnection == null)
            {
                return false;
            }

            SQLiteCommand command;
            if (parameters != null)
            {
                 command= Replace(dbConnection, sql, parameters);
            } else
            {
                command = new SQLiteCommand(sql, dbConnection);
            }

            using (var dbContext = new MyContext())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Logs.AddRange(new Log
                    {
                        Query = command.CommandText,
                        Date = DateTime.Now.ToString()
            });
                dbContext.SaveChanges();
            }

            command.ExecuteNonQuery();
            command.Dispose();
            dbConnection.Close();
            return true;
        }

        public static DataSet FetchDataset(string sql, ArrayList parameters = null)
        {
            var dbConnection = Connect();

            if (dbConnection == null)
            {
                return null;
            }

            SQLiteCommand command;
            if (parameters != null)
            {
                command = Replace(dbConnection, sql, parameters);
            }
            else
            {
                command = new SQLiteCommand(sql, dbConnection);
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dbConnection.Close();
            da.Dispose();
            return ds;
        }
    }
}
