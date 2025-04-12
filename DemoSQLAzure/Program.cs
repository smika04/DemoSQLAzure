using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        // Зчитуємо конфігурацію з файлу appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Отримуємо рядок підключення
        string connectionString = config.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("✅ Підключення встановлено.");
                // Запит для перевірки
                string query = "SELECT GETDATE() AS CurrentDateTime";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("📅 Поточна дата на сервері: " + reader["CurrentDateTime"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Помилка: " + ex.Message);
            }
        }
    }
}
