using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Performer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Record> Records { get; set; }

        public Performer()
        {
            Records = new List<Record>();
        }
    }
}
