using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Record> Records { get; set; }

        public Genre()
        {
            Records = new List<Record>();
        }

    }
}
