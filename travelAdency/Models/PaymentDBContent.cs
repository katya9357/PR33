using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using travelAdency.Models;

namespace travelAdency.Models
{
        public class PaymentDBContent : IDisposable
        {
            private string _connectionString;
            private OleDbConnection _connection;

            public PaymentDBContent(string fileOrServerOrConnection)
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

            public List<Payment> GetPayments()
            {
                var payments = new List<Payment>();

                try
                {
                    OpenConnection();
                    var command = new OleDbCommand("SELECT * FROM Оплата", _connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(new Payment
                            {
                                PaymentId = (int)reader["Код оплаты"],
                                TourId = (int)reader["Код путевки"],
                                PaymentDate = (DateTime)reader["Дата оплаты"],
                                Amount = (decimal)reader["Сумма"]
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                return payments;
            }

            public void AddPayment(Payment payment)
            {
                try
                {
                    OpenConnection();
                    var command = new OleDbCommand("INSERT INTO Оплата (Код путевки, Дата оплаты, Сумма) VALUES (?, ?, ?)", _connection);
                    command.Parameters.AddWithValue("?", payment.TourId);
                    command.Parameters.AddWithValue("?", payment.PaymentDate);
                    command.Parameters.AddWithValue("?", payment.Amount);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            public void UpdatePayment(Payment payment)
            {
                try
                {
                    OpenConnection();
                    var command = new OleDbCommand("UPDATE Оплата SET Код путевки = ?, Дата оплаты = ?, Сумма = ? WHERE Код оплаты = ?", _connection);
                    command.Parameters.AddWithValue("?", payment.TourId);
                    command.Parameters.AddWithValue("?", payment.PaymentDate);
                    command.Parameters.AddWithValue("?", payment.Amount);
                    command.Parameters.AddWithValue("?", payment.PaymentId);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            public void DeletePayment(int id)
            {
                try
                {
                    OpenConnection();
                    var command = new OleDbCommand("DELETE FROM Оплата WHERE Код оплаты = ?", _connection);
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

