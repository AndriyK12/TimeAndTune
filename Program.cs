using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;
using Npgsql;
using Faker;
using ConsoleTableExt;


namespace T_and_T_ADO_usage
{

    internal class Program
    {
        static int GetRandomUserId(NpgsqlConnection conn)
        {
            string query = "SELECT userId FROM \"user\" ORDER BY random() LIMIT 1";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }
        static void PrintDataTableInColumn(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Database=T&T;User Id=postgres;Password=Pa$$word";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Successfully connected to PostgreSQL.");
                for (int i = 0; i < 30; i++)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO \"user\" (username, email, coinsAmount, \"password\", passwordSalt) VALUES (@username, @email, @coinsAmount, @password, @passwordSalt)";
                        command.Parameters.AddWithValue("username", Faker.Internet.UserName());
                        command.Parameters.AddWithValue("email", Faker.Internet.Email());
                        command.Parameters.AddWithValue("coinsAmount", Faker.RandomNumber.Next(0, 1000));
                        command.Parameters.AddWithValue("password", BCrypt.Net.BCrypt.HashPassword(Faker.Lorem.Sentence()));
                        command.Parameters.AddWithValue("passwordSalt", Faker.Lorem.Sentence());
                        command.ExecuteNonQuery();
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        Random rand = new Random();
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO task (\"name\", description, dateOfCreation, expectedFinishTime, finishtime, priority, completed, executiontime, userIdRef) VALUES (@name, @description, @dateOfCreation, @expectedFinishTime, @finishtime, @priority, @completed, @executiontime, @userIdRef)";
                        command.Parameters.AddWithValue("name", Faker.Lorem.Sentence());
                        command.Parameters.AddWithValue("description", Faker.Lorem.Paragraph());
                        command.Parameters.AddWithValue("dateOfCreation", Faker.DateTimeFaker.DateTime());
                        command.Parameters.AddWithValue("expectedFinishTime", Faker.DateTimeFaker.DateTime());
                        command.Parameters.AddWithValue("finishtime", Faker.DateTimeFaker.DateTime());
                        command.Parameters.AddWithValue("priority", Faker.RandomNumber.Next(1, 3));
                        command.Parameters.AddWithValue("completed", Faker.Boolean.Random());
                        command.Parameters.AddWithValue("executiontime", TimeSpan.FromMilliseconds(rand.Next(0, int.MaxValue)));
                        command.Parameters.AddWithValue("userIdRef", GetRandomUserId(connection));
                        command.ExecuteNonQuery();
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO sound (soundName, audioFilePath, photoFilePath) VALUES (@soundName, @audioFilePath, @photoFilePath)";
                        command.Parameters.AddWithValue("soundName", Faker.Lorem.Sentence());
                        command.Parameters.AddWithValue("audioFilePath", "audio/" + Faker.Lorem.Sentence());
                        command.Parameters.AddWithValue("photoFilePath", "photos/" + Faker.Lorem.Sentence());
                        command.ExecuteNonQuery();
                    }
                }

                string userQuery = "SELECT * FROM \"user\"";
                using (NpgsqlDataAdapter userAdapter = new NpgsqlDataAdapter(userQuery, connection))
                {
                    DataTable userTable = new DataTable();
                    userAdapter.Fill(userTable);

                    Console.WriteLine("Data from 'user' table:");
                    PrintDataTableInColumn(userTable);
                }

                string taskQuery = "SELECT * FROM task";
                using (NpgsqlDataAdapter taskAdapter = new NpgsqlDataAdapter(taskQuery, connection))
                {
                    DataTable taskTable = new DataTable();
                    taskAdapter.Fill(taskTable);

                    Console.WriteLine("Data from 'task' table:");
                    PrintDataTableInColumn(taskTable);
                }

                string soundQuery = "SELECT * FROM sound";
                using (NpgsqlDataAdapter soundAdapter = new NpgsqlDataAdapter(soundQuery, connection))
                {
                    DataTable soundTable = new DataTable();
                    soundAdapter.Fill(soundTable);

                    Console.WriteLine("Data from 'sound' table:");
                    PrintDataTableInColumn(soundTable);
                }

                connection.Close();
                Console.WriteLine("Connection Closed.");
            }
            Console.ReadLine();
        }
    }
}
