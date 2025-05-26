using System;
using System.Collections.Generic;
using System.Data.OleDb;
using travelAdency.Models;

namespace travelAdency
{
    public class TuristyDBContext : IDisposable
    {
        private string _connectionString;
        private OleDbConnection _connection;

        public TuristyDBContext(string fileOrServerOrConnection)
        {
            _connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"D:\\учеба\\яПРАКТИКА\\33_33\\33\\Day 33\\travelAdency\\Data\\Turisty.mdb\";Persist Security Info=True";
            _connection = new OleDbConnection(_connectionString);
        }

        public OleDbConnection Connection => _connection;

        public void OpenConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public List<Turist> GetTurists()
        {
            var turists = new List<Turist>();

            try
            {
                OpenConnection();
                var command = new OleDbCommand("SELECT * FROM Туристы", _connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        turists.Add(new Turist
                        {
                            Id = (int)reader["Код туриста"],
                            FirstName = reader["Имя"].ToString(),
                            MiddleName = reader["Отчество"].ToString(),
                            LastName = reader["Фамилия"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return turists;
        }

        public void AddTurist(Turist turist)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("INSERT INTO Туристы (Имя, Отчество, Фамилия) VALUES (?, ?, ?)", _connection);
                command.Parameters.AddWithValue("?", turist.FirstName);
                command.Parameters.AddWithValue("?", turist.MiddleName);
                command.Parameters.AddWithValue("?", turist.LastName);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void UpdateTurist(Turist turist)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("UPDATE Туристы SET Имя = ?, Отчество = ?, Фамилия = ? WHERE [Код туриста] = ?", _connection);
                command.Parameters.AddWithValue("?", turist.FirstName);
                command.Parameters.AddWithValue("?", turist.MiddleName);
                command.Parameters.AddWithValue("?", turist.LastName);
                command.Parameters.AddWithValue("?", turist.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void DeleteTurist(int id)
        {
            try
            {
                OpenConnection();
                var command = new OleDbCommand("DELETE FROM Туристы WHERE [Код туриста] = ?", _connection);
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