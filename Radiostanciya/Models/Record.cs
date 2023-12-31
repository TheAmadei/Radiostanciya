﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
        public string Album { get; set; }
        public int Age { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime RecordDate { get; set; }
        public int TimeMin { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Рейтинг")]
        [Range(1, 5, ErrorMessage = "От 1 до 5")]
        public int Rating { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string DFormat(DateTime d)
        {
            return d.Year + "-" + String.Format("{0:00}", d.Month) + "-" + String.Format("{0:00}", d.Day);
        }
    }
}
