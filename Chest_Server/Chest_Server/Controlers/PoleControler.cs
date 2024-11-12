using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chest_Server.Controlers
{
    public class PoleControler
    {
        public static Chest_Server.Models.Pole PoleA = new Models.Pole(); //tu stoi figura
        public static Chest_Server.Models.Pole PoleB = new Models.Pole(); //tu sie chce ruszyc, chyba niepotrzebne
        public static Chest_Server.Models.Pole PoleC = new Models.Pole(); //sprawdzamy czy figura moze sie tu ruszyc
        public static Chest_Server.Models.Pole PoleAC = new Models.Pole(); //tu stoimy kiedy jestesmy w rekurencji
    }
}
//      PoleControler.PoleA