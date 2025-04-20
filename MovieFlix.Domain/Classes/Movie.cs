using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Domain.Classes
{
    public class Movie
    {
        public int ID { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public decimal Cost { get; set; }
    }
}
