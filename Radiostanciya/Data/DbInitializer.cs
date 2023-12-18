using Radiostanciya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Radiostanciya.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplContext db)
        {
            Console.WriteLine("Инициализация базы данных начата.");
            db.Database.EnsureCreated();
            if (db.Records.Any()) { return; }

            int positionsNumber = 10;
            int genresNumber = 20;
            int performersNumber = 10;
            int employeesNumber = 100;
            int recordsNumber = 10000;
            int schedulesNumber = 10000;

            // Заполняем таблицу Genres
            for (int id = 1; id <= genresNumber; id++)
            {
                db.Genres.Add(
                    new Genre
                    {
                        Name = $"Genre_{id}",
                        Description = $"Description for Genre_{id}"
                    });
            }
            db.SaveChanges();

            // Заполняем таблицу Performers
            for (int id = 1; id <= performersNumber; id++)
            {
                db.Performers.Add(
                    new Performer
                    {
                        Name = $"Performer_{id}",
                        Description = $"Description for Performer_{id}",
                    });
            }
            db.SaveChanges();

            // Заполняем таблицу Positions
            for (int id = 1; id <= positionsNumber; id++)
            {
                db.Positions.Add(
                    new Position
                    {
                        Name = $"Position_{id}",
                        Requirements = $"Requirements for Position_{id}",
                        Responsibilities = $"Responsibilities for Position_{id}",
                        Salary = id * 1000
                    });
            }
            db.SaveChanges();


            // Заполняем таблицу Employees
            var positions = db.Positions.ToList();

            if (positions.Count >= positionsNumber)
            {
                for (int id = 1; id <= employeesNumber; id++)
                {
                    int _id = id % positionsNumber;
                    db.Employees.Add(
                        new Employee
                        {
                            Name = $"Employee_{id}",
                            Age = 25 + id % 10,
                            Passport = $"Passport_{id}",
                            Address = $"Address_{id}",
                            PositionId = positions[_id].Id,
                            Position = positions[_id],
                            Sex = (id % 2 == 0) ? "Female" : "Male",
                        });
                }
                db.SaveChanges();
            }
            else
            {
                // Обработка случая, когда positions.Count меньше positionsNumber
                Console.WriteLine("Ошибка: Недостаточно данных в таблице Positions.");
            }


            var employees = db.Employees.ToList();
            var genres = db.Genres.ToList();
            var performers = db.Performers.ToList();
            for (int id = 1; id <= recordsNumber; id++)
            {
                int _idEmp = id % employeesNumber;
                int _idGenres = id % genresNumber;
                int _idPerf = id % performersNumber;
                db.Records.Add(
                    new Record
                    {
                        Age = 1800 + id % 200,
                        Album = $"Album_{id}",
                        EmployeeId = employees[_idEmp].Id,
                        Employee = employees[_idEmp],
                        Name = $"Record_{id}",
                        GenreId = genres[_idGenres].Id,
                        Genre = genres[_idGenres],
                        PerformerId = performers[_idPerf].Id,
                        Performer = performers[_idPerf],
                        RecordDate = new DateTime(2010 + id % 10, id % 12 + 1, id % 28 + 1),
                        TimeMin = id % 350,
                    });
            }
            db.SaveChanges();

            // Заполняем таблицу Schedules
            var records = db.Records.ToList();
            for (int id = 1; id <= schedulesNumber; id++)
            {
                int _idEmp = id % employeesNumber;
                int _idRec = id % recordsNumber;
                db.Schedules.Add(
                    new Schedule
                    {
                        Date = new DateTime(2010 + id % 10, id % 12 + 1, id % 28 + 1),
                        Start = new DateTime(2000, 1, 1).AddHours(id % 24).AddMinutes(id % 60),
                        End = new DateTime(2000, 1, 1).AddHours(id % 24 + 2).AddMinutes(id % 60),
                        EmployeeId = employees[_idEmp].Id,
                        Employee = employees[_idEmp],
                        RecordId = records[_idRec].Id,
                        Record = records[_idRec]
                    });
            }
            db.SaveChanges();


            Console.WriteLine("Инициализация базы данных завершена.");
        }
    }
}
