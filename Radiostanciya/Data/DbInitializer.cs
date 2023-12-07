using Radiostanciya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostanciya.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplContext db)
        {
            db.Database.EnsureCreated();
            if (db.Records.Any()) { return; }

            int employeesNumber = 30;
            int genresNumber = 20;
            int performersNumber = 10;
            int positionsNumber = 10;
            int recordsNumber = 30;
            int scheudulesNumber = 20;
            string voc = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            Random random = new Random(1);

            for (int id = 1; id <= genresNumber; id++)
            {
                db.Genres.Add(
                    new Genre
                    {
                        Name = GenRandomString(voc, 10),
                        Description = GenRandomString(voc, 10)
                    });
            }

            db.SaveChanges();

            for (int id = 1; id <= performersNumber; id++)
            {
                db.Performers.Add(
                    new Performer
                    {
                        Name = GenRandomString(voc, 10),
                        Description = GenRandomString(voc, 10),
                    });
            }

            db.SaveChanges();

            for (int id = 1; id <= positionsNumber; id++)
            {
                db.Positions.Add(
                    new Position
                    {
                        Name = GenRandomString(voc, 10),
                        Requirements  = GenRandomString(voc, 10),
                        Responsibilities = GenRandomString(voc, 10),
                        Salary = random.Next(100, 3000)
                    }) ;
            }
            db.SaveChanges();

            for (int id = 1; id <= employeesNumber; id++)
            {
                db.Employees.Add(
                    new Employee
                    {
                        Name = GenRandomString(voc, 10),
                        Age = random.Next(18, 60),
                        Passport = GenRandomString(voc, 10),
                        Address = GenRandomString(voc, 10),
                        PositionId = random.Next(1, positionsNumber - 1),
                        Sex = (random.Next(1, 2) == 1) ? "Female" : "Male",
                    });
            }
            db.SaveChanges();

            for (int id = 1; id <= scheudulesNumber; id++)
            {
                Schedule sc = new Schedule
                {
                    Date = new DateTime(random.Next(2010, 2020), random.Next(1, 12), random.Next(1, 28)),
                    Start = new DateTime(2000, 1, 1),
                    End = new DateTime(2000, 1, 1),
                    EmployeeId = random.Next(1, employeesNumber - 1),
                } ;
            sc.Start.AddHours(random.Next(0, 24));
            sc.Start.AddMinutes(random.Next(0, 60));
            sc.End.AddHours(random.Next(0, 24));
            sc.End.AddMinutes(random.Next(0, 60));
            db.Schedules.Add(sc);
                

            }
            db.SaveChanges();

            for (int id = 1; id <= recordsNumber; id++)
            {
                db.Records.Add(
                    new Record
                    {
                        Age = random.Next(1800, 2020),
                        Album = GenRandomString(voc, 10),
                        EmployeeId = random.Next(1, employeesNumber - 1),
                        Name = GenRandomString(voc, 10),
                        GenreId = random.Next(1, genresNumber - 1),
                        PerformerId = random.Next(1, performersNumber - 1),
                        RecordDate = new DateTime(random.Next(2010, 2020), random.Next(1, 12), random.Next(1, 28)),
                        TimeMin = random.Next(10, 360),
                    }) ;
            }
            db.SaveChanges();

        }
        static string GenRandomString(string Alphabet, int Length)
        {
            Random rnd = new Random();
            //объект StringBuilder с заранее заданным размером буфера под результирующую строку
            StringBuilder sb = new StringBuilder(Length - 1);
            //переменную для хранения случайной позиции символа из строки Alphabet
            int Position = 0;
            string ret = "";
            for (int i = 0; i < Length; i++)
            {
                //получаем случайное число от 0 до последнего
                //символа в строке Alphabet
                Position = rnd.Next(0, Alphabet.Length - 1);
                //добавляем выбранный символ в объект
                //StringBuilder
                ret = ret + Alphabet[Position];
            }
            return ret;
        }
    }
}
