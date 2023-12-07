using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Salary { get; set; }

        public string Responsibilities { get; set; }

        public string Requirements { get; set; }

        public List<Employee> Employees { get; set; }

        public Position()
        {
            Employees = new List<Employee>();
        }
    }
}
