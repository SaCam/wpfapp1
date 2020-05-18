using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WpfApp1.classBin
{
    class Player
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Country { get; set; }
        public object TeamId { get; set; }
        public object Img { get; set; }

        public Player(string name = null, string age = null, string country = null, object teamId = null, object img = null)
        {
            Name = name;
            Age = age;
            Country = country;
            TeamId = teamId;
            Img = img;
        }
    }
}
