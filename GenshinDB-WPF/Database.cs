using System;
using System.Collections.Generic;
using Npgsql;
using System.Windows;
using System.IO;

namespace GenshinDB_WPF
{
	public static class Database
	{
		static internal NpgsqlConnection connection;
		static private bool isThereConnection = false;
		internal static void Connect()
		{
			if (isThereConnection) connection.Dispose();
            string connectionstring = File.ReadAllText("../../../../APIREADKEY.txt");
            connection = new NpgsqlConnection(connectionstring);
			connection.Open();
			isThereConnection = true;
		}

		internal static NpgsqlDataReader Query(string sqlcommand)
		{
			
			try
			{
				using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, connection))
				{ //instead of doing "using" reader = command.ExecuteReader(), let it be so that the reader can be returned. But remember to close it afterwards. 
					NpgsqlDataReader reader = command.ExecuteReader();
					reader.Read(); //VERY IMPORTANT! Needed to go to the first row.
					return reader; //Note: Close the reader when done.
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			return null;
			
		}

        internal static string Query(string sqlcommand, int column)
        {
			string temp = "";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, connection))
                {
                    using(NpgsqlDataReader reader = command.ExecuteReader())
					{
						reader.Read();
						return reader[column].ToString();
					}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;

        }

        internal static List<string> ListQuery(string sqlcommand, int column)
        {
            List<string> result = new List<string>();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, connection))
                {
					using (NpgsqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(reader[column].ToString());
						}
					}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }
    }
}
