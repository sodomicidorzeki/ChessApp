using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chest_Server.Models  //     Chest_Server.Models.Kierunek.
{
    public class Kierunek
    {
        public int Wx { get; set; }
        public int Wy { get; set; }
        public int Wskok { get; set; }
        public Kierunek(int a, int b, int c)
        { 
            Wx = a;
            Wy = b;
            Wskok = c;
        }
    }
}
