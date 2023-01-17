using System;
using System.Collections.Generic;
using Npgsql;
using System.Windows;

namespace GenshinDB_WPF
{
	public static class Database
	{
		static internal NpgsqlConnection connection;
		static private bool isThereConnection = false;
		internal static void Connect()
		{
			if (isThereConnection) connection.Dispose();
			string connectionstring = @"Server=containers-us-west-53.railway.app;Port=7418;Database=railway;User Id=dbuser;Password=genshin123;";
			connection = new NpgsqlConnection(connectionstring);
			connection.Open();
			isThereConnection = true;
		}

		internal static string Query(string sqlcommand, int column)
		{
			string result = "";
			try
			{
				using (NpgsqlCommand command = new NpgsqlCommand(sqlcommand, connection))
				{
					result = command.ExecuteReader()[column].ToString();
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			
			return result;
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
