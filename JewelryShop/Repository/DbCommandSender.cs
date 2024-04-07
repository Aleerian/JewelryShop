using JewelryShop.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JewelryShop.Repository
{
    public class DbCommandSender
    {
        public User SignIn (string login, string password)
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"User\" WHERE login=@login AND password=@password");
            command.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, login);
            command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    if (result.HasRows)
                    {
                        result.Read();

                        User user = new User
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Surname = result.GetString(result.GetOrdinal("Surname")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                            Patronymic = result.GetString(result.GetOrdinal("Patronymic")),
                            Login = result.GetString(result.GetOrdinal("Login")),
                            Password = result.GetString(result.GetOrdinal("Password")),
                            Role = result.GetInt32(result.GetOrdinal("Role"))
                        };

                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }
    }
}
