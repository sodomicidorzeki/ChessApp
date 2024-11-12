using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chest_Server.Models
{
    internal class Szachownica      //        Chest_Server.Models.Szachownica.Plansza
    {
        static public string[] Plansza = [
            "w", "s", "g", "h", "k", "g", "s", "w", //8
            "p", "p", "p", "p", "p", "p", "p", "p", //7
            "*", "*", "*", "*", "*", "*", "*", "*", //6
            "*", "*", "*", "*", "k", "*", "W", "*", //5
            "*", "*", "*", "*", "*", "*", "*", "*", //4
            "*", "*", "*", "*", "*", "*", "*", "*", //3
            "P", "P", "P", "P", "P", "P", "P", "P", //2
            "W", "S", "G", "H", "K", "G", "S", "W", //1
        ];// 1    2    3    4    5    6    7    8



        /*public static void ustaw_szachownice()
        {
            Plansza[0] = "K";
            Plansza[1] = "*";
            Plansza[2] = "*";
            Plansza[3] = "*";
            Plansza[4] = "*";
            Plansza[5] = "*";
            Plansza[6] = "*";
            Plansza[7] = "*";
            Plansza[8] = "*";
        }*/
        public static void Wypisz()
        {
            int osiem = 8;
            int k = 0;
            string pomocniczy = Convert.ToString(osiem)+ " ";
            for (int i = 0; i < osiem; i++)
            {
                for (int j = 0; j < osiem; j++)
                {
                    pomocniczy += " " + Plansza[k];
                    k++;
                }
                Console.WriteLine(pomocniczy);
                pomocniczy = Convert.ToString(osiem - i-1) + " ";
            }
            pomocniczy = "   ";
            for (k = 1; k < osiem+1; k++)
            {
                pomocniczy += Convert.ToString(k) + " ";
            }
            Console.WriteLine(pomocniczy);

        }
        public static void Wykonanie_ruchu(int a, int b)
        {
            Plansza[b] = Plansza[a];
            Plansza[a] = "*";
        }
        public static int Daj_wiersz(int a)
        {
            if (a < 0)
                a = -a;
            int b = 8;
            int i = 8;
            while (true)
            {
                if(a<b)
                {
                    return i;
                }
                b += 8;
                i -= 1;
            }
            /*
            if (a <= 3)
            {
                return 4;
            }
            if (a <= 7)
            {
                return 3;
            }
            if (a <= 10)
            {
                return 2;
            }
            if (a <= 15)
            {
                return 1;
            }
            return 0;*/
        }
        public static int Daj_kolumne(int a)
        {
            if (a < 0)
                a = -a;
            int b = (a % 8) + 1;
            return b;
        }


    }
}
/*
 * 4 [ 0] [ 1] [ 2] [ 3]
 * 3 [ 4] [ 5] [ 6] [ 7]
 * 2 [ 8] [ 9] [10] [11]
 * 1 [12] [13] [14] [15]
 *    1    2    3    4
 *  krol-K hetman-H goniec-G skoczek-S wieza-W pionek-P
 *  duza litera - biala bierka
 *  mala litera - czarna bierka
 * 
 * 
       internal class Szachownica      //        Chest_Server.Models.Szachownica.Plansza
    {
        static public string[] Plansza = [
            "*", "*", "*", "*", "*", "*", "*", "*", //8
            "*", "*", "*", "*", "*", "*", "*", "*", //7
            "*", "*", "*", "*", "*", "*", "*", "*", //6
            "*", "*", "*", "*", "*", "*", "*", "*", //5
            "*", "*", "*", "*", "*", "*", "*", "*", //4
            "*", "*", "*", "*", "*", "*", "*", "*", //3
            "*", "*", "*", "*", "*", "*", "*", "*", //2
            "*", "*", "*", "*", "*", "*", "*", "*", //1
        ];// 1    2    3    4    5    6    7    8
 * 
 * 
 * 
 * 
 */