using System;
using System.Collections.Generic;
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
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string DFormat(DateTime d)
        {
            return d.Year + "-" + String.Format("{0:00}", d.Month) + "-" + String.Format("{0:00}", d.Day);
        }
    }
}
