using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAdency.Models
{
    public class TourDBContent : IDisposable
    {
        private string _connectionString;
        private OleDbConnection _connection;

        public TourDBContent(string fileOrServerOrConnection)
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

        public List<Tour> GetTours()
        {
            var tours = new List<Tour>();

            try
            {
                OpenConnection();
                var command = new OleDbCommand("SELECT * FROM Туры", _connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tours.Add(new Tour
                        {
                            TourId = (int)reader["Код тура"],
                            Name = reader["Название"].ToString(),
                            Price = (decimal)reader["Цена"],
                            Information = reader["Информация"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return tours;
        }

        public void AddTour(Tour tour)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("INSERT INTO Туры (Название, Цена, Информация) VALUES (?, ?, ?)", _connection);
                command.Parameters.AddWithValue("?", tour.Name);
                command.Parameters.AddWithValue("?", tour.Price);
                command.Parameters.AddWithValue("?", tour.Information);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void UpdateTour(Tour tour)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("UPDATE Туры SET Название = ?, Цена = ?, Информация = ? WHERE [Код тура] = ?", _connection);
                command.Parameters.AddWithValue("?", tour.Name);
                command.Parameters.AddWithValue("?", tour.Price);
                command.Parameters.AddWithValue("?", tour.Information);
                command.Parameters.AddWithValue("?", tour.TourId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void DeleteTour(int id)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("DELETE FROM Туры WHERE [Код тура] = ?", _connection);
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
