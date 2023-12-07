using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string DFormat(DateTime d)
        {
            return d.Year + "-" + String.Format("{0:00}" , d.Month) + "-" + String.Format("{0:00}", d.Day);
        }

        public string TFormat(DateTime d)
        {
            return String.Format("{0:00}", d.Hour) + ":" + String.Format("{0:00}", d.Minute);
        }
    }
}
