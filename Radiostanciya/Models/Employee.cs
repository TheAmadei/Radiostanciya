using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }
        public string Address { get; set; }

        public string Passport { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public List<Schedule> Scheudules { get; set; }
        public List<Record> Records { get; set; }


        public Employee()
        {
            Scheudules = new List<Schedule>();
            Records = new List<Record>();
        }
    }
}
