using Chest_Server.Controlers;
using Chest_Server.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        //Szachownica.ustaw_szachownice();
        //MoveControler.Czy_jest_szach(21); //NIE DZIALA


        /*Szachownica.Wypisz();
        int laia = 28;
        Console.WriteLine($"Dla bierki na polu {laia}");
        Chest_Server.Controlers.MoveControler.wybralem_pole_z_figura(laia);
        for (int i = 0; i < MozliweRuchy.ruchy.Count(); i++)
        {
            Console.WriteLine(MozliweRuchy.ruchy[i]);
        }
        /*
        Szachownica.Wypisz();
        Szachownica.Wykonanie_ruchu(2, 6);*/

        /*Zamek[] Polska = new Zamek[4];
        Polska[0] = new Zamek(1, 2);
        Polska[1] = new Zamek(1, 3);
        Polska[2] = new Zamek(1, 4);
        Polska[3] = new Zamek(1, 5);

        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine(BardzoWesTab[i, j]);
            }
        }*/
        //Console.WriteLine("x a  b  c\r\n3 *  *  *\r\n2 *  *  *\r\n1 *  *  *");
        //Szachownica.wyswietl_szachownice();
    }

    public class Zamek
    {
        int a, b;
        public Zamek(int A, int B) { a = A; b = B; }
    }

}
//Console.WriteLine(BardzoWesTab[0,0]);


//BardzoWesTab[0]][0] = 1;
//Console.WriteLine("Hello, World!");

