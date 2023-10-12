using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

namespace T_and_T_ADO_usage
{

    internal class Program
    {
        static string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, characters.Length);
                stringBuilder.Append(characters[index]);
            }

            return stringBuilder.ToString();
        }
        static int GenerateRandomInt(int min=0, int max=1000)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
        public static void AddUser(NpgsqlConnection connection, string username, string email, int coins_amount, string password)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO public.\"user\" (username, email, coins_amount, password) VALUES (@username, @email, @coins_amount, @password)";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@coins_amount", coins_amount);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();
        }
        public static void FillUserWithRandomInfo(NpgsqlConnection connection, int number_of_rows) {
            string username;
            string email;
            int coins_amount;
            string password;
            for (int i = 0; i< number_of_rows; i++)
            {
                username = GenerateRandomString(5);
                email = $"mail{GenerateRandomInt(0, 100000000)+i}"+"@gmail.com";
                coins_amount = GenerateRandomInt();
                password = GenerateRandomString(8);
                AddUser(connection, username, email, coins_amount, password);
            }
        }
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Database=T&T;User Id=postgres;Password=Pa$$word";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected to PostgreSQL.");
                    //AddUser(connection, "test2", "test2@email.com", 10, "228822");
                    FillUserWithRandomInfo(connection,30);

                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.\"user\"", connection);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Info about users :");

                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["user_id"]}");
                        Console.WriteLine($"username: {reader["username"]}");
                        Console.WriteLine($"email: {reader["email"]}");
                        Console.WriteLine($"coins_amount: {reader["coins_amount"]}");
                        Console.WriteLine($"password: {reader["password"]}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                connection.Close();
                Console.WriteLine("Connection Closed.");
            }
            Console.ReadLine();
        }
    }
}
