using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.classBin
{
    class Player
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Country { get; set; }
        public object TeamId { get; set; }

        public Player(string name, string age, string country, object teamId = null)
        {
            Name = name;
            Age = age;
            Country = country;
            TeamId = teamId;
        }
    }
}
