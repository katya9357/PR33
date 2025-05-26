using System;
using travelAdency;
using travelAdency.Models;

namespace Task_Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"D:\учеба\яПРАКТИКА\33_33\33\Day 33\TuristyDAL\Data\Turisty.mdb";
            var dbContext = new TuristyDBContext(connectionString);
            var paymentRepo = new PaymentDBContent(connectionString);
            var seasonRepo = new SeasonDBContent(connectionString);
            var touristInfoRepo = new TouristInfoDBContent(connectionString);
            var tourRepo = new TourDBContent(connectionString);

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать туристов");
                Console.WriteLine("2. Добавить туриста");
                Console.WriteLine("3. Изменить туриста");
                Console.WriteLine("4. Удалить туриста");
                Console.WriteLine("5. Показать оплаты");
                Console.WriteLine("6. Добавить оплату");
                Console.WriteLine("7. Изменить оплату");
                Console.WriteLine("8. Удалить оплату");
                Console.WriteLine("9. Показать сезоны");
                Console.WriteLine("10. Добавить сезон");
                Console.WriteLine("11. Изменить сезон");
                Console.WriteLine("12. Удалить сезон");
                Console.WriteLine("13. Показать информацию о туристах");
                Console.WriteLine("14. Добавить информацию о туристе");
                Console.WriteLine("15. Изменить информацию о туристе");
                Console.WriteLine("16. Удалить информацию о туристе");
                Console.WriteLine("17. Показать туры");
                Console.WriteLine("18. Добавить тур");
                Console.WriteLine("19. Изменить тур");
                Console.WriteLine("20. Удалить тур");
                Console.WriteLine("21. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var turists = dbContext.GetTurists();
                        foreach (var turist in turists)
                        {
                            Console.WriteLine($"{turist.Id}: {turist.FirstName} {turist.LastName}");
                        }
                        break;

                    case "2":
                        var newTurist = new Turist();
                        Console.Write("Введите имя: ");
                        newTurist.FirstName = Console.ReadLine();
                        Console.Write("Введите отчество: ");
                        newTurist.MiddleName = Console.ReadLine();
                        Console.Write("Введите фамилию: ");
                        newTurist.LastName = Console.ReadLine();
                        dbContext.AddTurist(newTurist);
                        Console.WriteLine("Турист добавлен.");
                        break;

                    case "3":
                        Console.Write("Введите ID туриста для изменения: ");
                        int updateId = int.Parse(Console.ReadLine());
                        var updateTurist = new Turist { Id = updateId };
                        Console.Write("Введите новое имя: ");
                        updateTurist.FirstName = Console.ReadLine();
                        Console.Write("Введите новое отчество: ");
                        updateTurist.MiddleName = Console.ReadLine();
                        Console.Write("Введите новую фамилию: ");
                        updateTurist.LastName = Console.ReadLine();
                        dbContext.UpdateTurist(updateTurist);
                        Console.WriteLine("Турист обновлен.");
                        break;

                    case "4":
                        Console.Write("Введите ID туриста для удаления: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        dbContext.DeleteTurist(deleteId);
                        Console.WriteLine("Турист удален.");
                        break;

                    case "5":
                        var payments = paymentRepo.GetPayments();
                        foreach (var payment in payments)
                        {
                            Console.WriteLine($"ID: {payment.PaymentId}, Код путёвки: {payment.TourId}, Дата: {payment.PaymentDate}, Сумма: {payment.Amount}");
                        }
                        break;

                    case "6":
                        var newPayment = new Payment();
                        Console.Write("Введите Код путёвки: ");
                        newPayment.TourId = int.Parse(Console.ReadLine());
                        Console.Write("Введите Дату оплаты: ");
                        newPayment.PaymentDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите Сумму: ");
                        newPayment.Amount = decimal.Parse(Console.ReadLine());
                        paymentRepo.AddPayment(newPayment);
                        Console.WriteLine("Оплата добавлена.");
                        break;

                    case "7":
                        Console.Write("Введите ID оплаты для изменения: ");
                        int updatePaymentId = int.Parse(Console.ReadLine());
                        var updatePayment = new Payment { PaymentId = updatePaymentId };
                        Console.Write("Введите новый Код путёвки: ");
                        updatePayment.TourId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новую Дату оплаты: ");
                        updatePayment.PaymentDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите новую Сумму: ");
                        updatePayment.Amount = decimal.Parse(Console.ReadLine());
                        paymentRepo.UpdatePayment(updatePayment);
                        Console.WriteLine("Оплата обновлена.");
                        break;

                    case "8":
                        Console.Write("Введите ID оплаты для удаления: ");
                        int deletePaymentId = int.Parse(Console.ReadLine());
                        paymentRepo.DeletePayment(deletePaymentId);
                        Console.WriteLine("Оплата удалена.");
                        break;

                    case "9":
                        var seasons = seasonRepo.GetSeasons();
                        foreach (var season in seasons)
                        {
                            Console.WriteLine($"ID: {season.SeasonId}, Код тура: {season.TourId}, Дата начала: {season.StartDate}, Дата конца: {season.EndDate}, Сезон закрыт: {season.IsClosed}, Количество мест: {season.AvailableSeats}");
                        }
                        break;

                    case "10":
                        var newSeason = new Season();
                        Console.Write("Введите Код тура: ");
                        newSeason.TourId = int.Parse(Console.ReadLine());
                        Console.Write("Введите Дату начала: ");
                        newSeason.StartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите Дату конца: ");
                        newSeason.EndDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите статус сезона (true/false): ");
                        newSeason.IsClosed = bool.Parse(Console.ReadLine());
                        Console.Write("Введите Количество мест: ");
                        newSeason.AvailableSeats = int.Parse(Console.ReadLine());
                        seasonRepo.AddSeason(newSeason);
                        Console.WriteLine("Сезон добавлен.");
                        break;

                    case "11":
                        Console.Write("Введите ID сезона для изменения: ");
                        int updateSeasonId = int.Parse(Console.ReadLine());
                        var updateSeason = new Season { SeasonId = updateSeasonId };
                        Console.Write("Введите новый Код тура: ");
                        updateSeason.TourId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новую Дату начала: ");
                        updateSeason.StartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите новую Дату конца: ");
                        updateSeason.EndDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите новый статус сезона (true/false): ");
                        updateSeason.IsClosed = bool.Parse(Console.ReadLine());
                        Console.Write("Введите новое Количество мест: ");
                        updateSeason.AvailableSeats = int.Parse(Console.ReadLine());
                        seasonRepo.UpdateSeason(updateSeason);
                        Console.WriteLine("Сезон обновлен.");
                        break;

                    case "12":
                        Console.Write("Введите ID сезона для удаления: ");
                        int deleteSeasonId = int.Parse(Console.ReadLine());
                        seasonRepo.DeleteSeason(deleteSeasonId);
                        Console.WriteLine("Сезон удален.");
                        break;

                    case "13":
                        var touristInfos = touristInfoRepo.GetTouristInfos();
                        foreach (var info in touristInfos)
                        {
                            Console.WriteLine($"Код туриста: {info.TouristId}, Серия паспорта: {info.PassportSeries}, Город: {info.City}, Страна: {info.Country}, Телефон: {info.Phone}, Индекс: {info.PostalCode}");
                        }
                        break;

                    case "14":
                        var newTouristInfo = new TouristInfo();
                        Console.Write("Введите Код туриста: ");
                        newTouristInfo.TouristId = int.Parse(Console.ReadLine());
                        Console.Write("Введите Серия паспорта: ");
                        newTouristInfo.PassportSeries = Console.ReadLine();
                        Console.Write("Введите Город: ");
                        newTouristInfo.City = Console.ReadLine();
                        Console.Write("Введите Страна: ");
                        newTouristInfo.Country = Console.ReadLine();
                        Console.Write("Введите Телефон: ");
                        newTouristInfo.Phone = Console.ReadLine();
                        Console.Write("Введите Индекс: ");
                        newTouristInfo.PostalCode = Console.ReadLine();
                        touristInfoRepo.AddTouristInfo(newTouristInfo);
                        Console.WriteLine("Информация о туристе добавлена.");
                        break;

                    case "15":
                        Console.Write("Введите Код туриста для изменения: ");
                        int updateTouristInfoId = int.Parse(Console.ReadLine());
                        var updateTouristInfo = new TouristInfo { TouristId = updateTouristInfoId };
                        Console.Write("Введите новую Серия паспорта: ");
                        updateTouristInfo.PassportSeries = Console.ReadLine();
                        Console.Write("Введите новый Город: ");
                        updateTouristInfo.City = Console.ReadLine();
                        Console.Write("Введите новую Страна: ");
                        updateTouristInfo.Country = Console.ReadLine();
                        Console.Write("Введите новый Телефон: ");
                        updateTouristInfo.Phone = Console.ReadLine();
                        Console.Write("Введите новый Индекс: ");
                        updateTouristInfo.PostalCode = Console.ReadLine();
                        touristInfoRepo.UpdateTouristInfo(updateTouristInfo);
                        Console.WriteLine("Информация о туристе обновлена.");
                        break;

                    case "16":
                        Console.Write("Введите Код туриста для удаления: ");
                        int deleteTouristInfoId = int.Parse(Console.ReadLine());
                        touristInfoRepo.DeleteTouristInfo(deleteTouristInfoId);
                        Console.WriteLine("Информация о туристе удалена.");
                        break;

                    case "17":
                        var tours = tourRepo.GetTours();
                        foreach (var tour in tours)
                        {
                            Console.WriteLine($"Код тура: {tour.TourId}, Название: {tour.Name}, Цена: {tour.Price}, Информация: {tour.Information}");
                        }
                        break;

                    case "18":
                        var newTour = new Tour();
                        Console.Write("Введите Название: ");
                        newTour.Name = Console.ReadLine();
                        Console.Write("Введите Цена: ");
                        newTour.Price = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите Информация: ");
                        newTour.Information = Console.ReadLine();
                        tourRepo.AddTour(newTour);
                        Console.WriteLine("Тур добавлен.");
                        break;

                    case "19":
                        Console.Write("Введите Код тура для изменения: ");
                        int updateTourId = int.Parse(Console.ReadLine());
                        var updateTour = new Tour { TourId = updateTourId };
                        Console.Write("Введите новое Название: ");
                        updateTour.Name = Console.ReadLine();
                        Console.Write("Введите новую Цена: ");
                        updateTour.Price = decimal.Parse(Console.ReadLine());
                        Console.Write("Введите новую Информация: ");
                        updateTour.Information = Console.ReadLine();
                        tourRepo.UpdateTour(updateTour);
                        Console.WriteLine("Тур обновлен.");
                        break;

                    case "20":
                        Console.Write("Введите Код тура для удаления: ");
                        int deleteTourId = int.Parse(Console.ReadLine());
                        tourRepo.DeleteTour(deleteTourId);
                        Console.WriteLine("Тур удален.");
                        break;

                    case "21":
                        return; 

                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear(); 
            }
        }
    }
}