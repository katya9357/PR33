using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace travelAdency.Models
{
    public class SeasonDBContent : IDisposable
    {
        private string _connectionString;
        private OleDbConnection _connection;

        public SeasonDBContent(string fileOrServerOrConnection)
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

        public List<Season> GetSeasons()
        {
            var seasons = new List<Season>();

            try
            {
                OpenConnection();
                var command = new OleDbCommand("SELECT * FROM Сезоны", _connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add(new Season
                        {
                            SeasonId = (int)reader["Код сезона"],
                            TourId = (int)reader["Код тура"],
                            StartDate = (DateTime)reader["Дата начала"],
                            EndDate = (DateTime)reader["Дата конца"],
                            IsClosed = (bool)reader["Сезон закрыт"],
                            AvailableSeats = (int)reader["Количество мест"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return seasons;
        }

        public void AddSeason(Season season)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("INSERT INTO Сезоны ([Код тура], [Дата начала], [Дата конца], [Сезон закрыт], [Количество мест]) VALUES (?, ?, ?, ?, ?)", _connection);
                command.Parameters.AddWithValue("?", season.TourId);
                command.Parameters.AddWithValue("?", season.StartDate);
                command.Parameters.AddWithValue("?", season.EndDate);
                command.Parameters.AddWithValue("?", season.IsClosed);
                command.Parameters.AddWithValue("?", season.AvailableSeats);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void UpdateSeason(Season season)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("UPDATE Сезоны SET [Код тура] = ?, [Дата начала] = ?, [Дата конца] = ?, [Сезон закрыт] = ?, [Количество мест] = ? WHERE [Код сезона] = ?", _connection);
                command.Parameters.AddWithValue("?", season.TourId);
                command.Parameters.AddWithValue("?", season.StartDate);
                command.Parameters.AddWithValue("?", season.EndDate);
                command.Parameters.AddWithValue("?", season.IsClosed);
                command.Parameters.AddWithValue("?", season.AvailableSeats);
                command.Parameters.AddWithValue("?", season.SeasonId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void DeleteSeason(int id)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("DELETE FROM Сезоны WHERE [Код сезона] = ?", _connection);
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
