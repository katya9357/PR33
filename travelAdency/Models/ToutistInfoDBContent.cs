using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace travelAdency.Models
{
    public class TouristInfoDBContent : IDisposable
    {
        private string _connectionString;
        private OleDbConnection _connection;

        public TouristInfoDBContent(string fileOrServerOrConnection)
        {
            _connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"D:\\учеба\\яПРАКТИКА\\33_33\\33\\Day 33\\travelAdency\\Data\\Turisty.mdb\";Persist Security Info=True";
            _connection = new OleDbConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public List<TouristInfo> GetTouristInfos()
        {
            var touristInfos = new List<TouristInfo>();

            try
            {
                OpenConnection();
                var command = new OleDbCommand("SELECT * FROM [Информация о туристах]", _connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        touristInfos.Add(new TouristInfo
                        {
                            TouristId = (int)reader["Код туриста"],
                            PassportSeries = reader["Серия паспорта"].ToString(),
                            City = reader["Город"].ToString(),
                            Country = reader["Страна"].ToString(),
                            Phone = reader["Телефон"].ToString(),
                            PostalCode = reader["Индекс"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return touristInfos;
        }

        public void AddTouristInfo(TouristInfo touristInfo)
        {
            try
            {
                OpenConnection();

                var checkCommand = new OleDbCommand("SELECT COUNT(*) FROM [Информация о туристах] WHERE [Код туриста] = ?", _connection);
                checkCommand.Parameters.AddWithValue("?", touristInfo.TouristId);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    Console.WriteLine($"Информация о туристе с Кодом {touristInfo.TouristId} уже существует.");
                    return; 
                }

                var command = new OleDbCommand("INSERT INTO [Информация о туристах] ([Код туриста], [Серия паспорта], Город, Страна, Телефон, Индекс) VALUES (?, ?, ?, ?, ?, ?)", _connection);
                command.Parameters.AddWithValue("?", touristInfo.TouristId);
                command.Parameters.AddWithValue("?", touristInfo.PassportSeries);
                command.Parameters.AddWithValue("?", touristInfo.City);
                command.Parameters.AddWithValue("?", touristInfo.Country);
                command.Parameters.AddWithValue("?", touristInfo.Phone);
                command.Parameters.AddWithValue("?", touristInfo.PostalCode);
                command.ExecuteNonQuery();

                Console.WriteLine("Информация о туристе добавлена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void UpdateTouristInfo(TouristInfo touristInfo)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("UPDATE [Информация о туристах] SET [Серия паспорта] = ?, Город = ?, Страна = ?, Телефон = ?, Индекс = ? WHERE [Код туриста] = ?", _connection);
                command.Parameters.AddWithValue("?", touristInfo.PassportSeries);
                command.Parameters.AddWithValue("?", touristInfo.City);
                command.Parameters.AddWithValue("?", touristInfo.Country);
                command.Parameters.AddWithValue("?", touristInfo.Phone);
                command.Parameters.AddWithValue("?", touristInfo.PostalCode);
                command.Parameters.AddWithValue("?", touristInfo.TouristId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void DeleteTouristInfo(int id)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("DELETE FROM [Информация о туристах] WHERE [Код туриста] = ?", _connection);
                command.Parameters.AddWithValue("?", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
