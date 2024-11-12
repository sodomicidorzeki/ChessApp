using Chest_Server.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Chest_Server.Controlers
{
    public class MoveControler
    {
        static Chest_Server.Models.Kierunek[] Kierunki = {
        new Chest_Server.Models.Kierunek(0, 1, -8), //Gora
        new Chest_Server.Models.Kierunek(1, 1, 1-8), //Gora_Prawo
        new Chest_Server.Models.Kierunek(1, 0, 1), //Prawo
        new Chest_Server.Models.Kierunek(1, -1, 1+8), //Dol_Prawo
        new Chest_Server.Models.Kierunek(0, -1, 8), //Dol
        new Chest_Server.Models.Kierunek(-1, -1, -1+8), //Dol_Lewo
        new Chest_Server.Models.Kierunek(-1, 0, -1), //Lewo
        new Chest_Server.Models.Kierunek(-1, 1, -1-8), //Gora_Lewo
        new Chest_Server.Models.Kierunek(-1, 2, - 2 * 8 - 1), //Lewa Gora, Skoczek1 
        new Chest_Server.Models.Kierunek(1, 2,  - 2 * 8 + 1), //Skoczek2
        new Chest_Server.Models.Kierunek(2, 1,  + 1 + 1 - 8), //Skoczek3
        new Chest_Server.Models.Kierunek(2, -1,  + 1 + 1 + 8), //4 
        new Chest_Server.Models.Kierunek(1, -2,  + 2 * 8 + 1), //5
        new Chest_Server.Models.Kierunek(1, -2,  + 2 * 8 - 1), //6
        new Chest_Server.Models.Kierunek(-2, -1,  - 1 - 1 + 8), //7
        new Chest_Server.Models.Kierunek(-2, 1,  - 1 - 1 - 8)}; //8
        static bool szach = false; //czy jest szach
        static int czyjRuch = 1; //biale 1, czarne 0
        static int pozycjaKrolaCzarnego = 4;
        static int pozycjaKrolaBialego = 60;
        //int a - pole na ktorym jestem
        //int b - pole na ktore chce sie ruszyc
        static bool sprawdzaczRekurencji = false;
        public static int wybralem_pole_z_figura(int a)
        {
            string figura = Chest_Server.Models.Szachownica.Plansza[a];
            if (figura == "*")
            {
                Console.WriteLine("Wybrane pole startowe jest puste.");
                return 1;
            }
            Pole polePomocnicze = new Pole();
            polePomocnicze.NumerPola = a;
            polePomocnicze.x = Chest_Server.Models.Szachownica.Daj_kolumne(a);
            polePomocnicze.y = Chest_Server.Models.Szachownica.Daj_wiersz(a);
            Chest_Server.Controlers.PoleControler.PoleA = polePomocnicze;
            if (figura == "K" || figura == "k")
                Krol(a);
            if (figura == "H" || figura == "h")
                Hetman(a);
            if (figura == "W" || figura == "w")
                Wieza(a);
            if (figura == "G" || figura == "g")
                Goniec(a);
            if (figura == "S" || figura == "s")
                Skoczek(a);
            if (figura == "P" || figura == "p")
                Pionek(a, figura);
            return 0;
        }
        public static bool Nie_ma_szacha(int k) //k - pozycja krola
        {
            if (Chest_Server.Models.Szachownica.Plansza[k] != "k" && Chest_Server.Models.Szachownica.Plansza[k] != "K")
            {
                Console.WriteLine("Na polu nie ma krola");
                return false;
            }
            List<int> ruchyZapis = MozliweRuchy.ruchy;
            MozliweRuchy.ruchy.Clear();
            int kolor = Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[k]);
            Pole polePomocnicze = new Pole();
            Pole poleKopiaA = Chest_Server.Controlers.PoleControler.PoleA;
            polePomocnicze.NumerPola = k;
            polePomocnicze.x = Chest_Server.Models.Szachownica.Daj_kolumne(k);
            polePomocnicze.y = Chest_Server.Models.Szachownica.Daj_wiersz(k);
            Chest_Server.Controlers.PoleControler.PoleA = polePomocnicze;
            int hj;
            int jk;
            if (kolor == 0)
            {// 3 5
                hj = 3;
                jk = 5;
            }
            else
            {// 1 7
                hj = 1;
                jk = 7;
            }
            for (int i = 8; i < 16; i++) //skoczek
            {
                if (Czy_pole_istnieje(PoleControler.PoleA, Kierunki[i]))
                {
                    if (Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola] == "s" || Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola] == "S")
                    {
                        if(Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]) != 3 && kolor != Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]))
                        {
                            //szach
                            Console.WriteLine("Jest szach od skoczka");
                            MozliweRuchy.ruchy = ruchyZapis;
                            Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
                            return false;
                        }
                    }
                }

            }
            string pomocniczy;
            for (int i = 0; i < 8; i+=2) //wieza
            {
                Rekurencja(k, i);
                sprawdzaczRekurencji = false;

                if(MozliweRuchy.ruchy.Count() != 0)
                {
                    pomocniczy = Chest_Server.Models.Szachownica.Plansza[MozliweRuchy.ruchy[MozliweRuchy.ruchy.Count() - 1]];
                    if (pomocniczy == "w" || pomocniczy == "W" || pomocniczy == "h" || pomocniczy == "H")
                    {
                        //szach
                        Console.WriteLine("Jest szach od wiezy (lub hetmana)");
                        MozliweRuchy.ruchy = ruchyZapis;
                        Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
                        return false;
                    }
                }

                MozliweRuchy.ruchy.Clear();
            }
            for (int i = 1; i < 9; i += 2) //goniec
            {
                Rekurencja(k, i);
                sprawdzaczRekurencji = false;

                if (MozliweRuchy.ruchy.Count() != 0)
                {
                    pomocniczy = Chest_Server.Models.Szachownica.Plansza[MozliweRuchy.ruchy[MozliweRuchy.ruchy.Count() - 1]];
                    if (pomocniczy == "g" || pomocniczy == "G" || pomocniczy == "h" || pomocniczy == "H")
                    {
                        //szach
                        Console.WriteLine("Jest szach od gonca (lub hetmana)");
                        MozliweRuchy.ruchy = ruchyZapis;
                        Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
                        return false;
                    }
                }
                MozliweRuchy.ruchy.Clear();
            }
            if (Czy_pole_istnieje(PoleControler.PoleA, Kierunki[hj])) //pionek
            {
                if (Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]) != 3 && kolor != Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]))
                {
                    //szach
                    Console.WriteLine("Jest szach od pionka");
                    MozliweRuchy.ruchy = ruchyZapis;
                    Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
                    return false;
                }
            }
            if (Czy_pole_istnieje(PoleControler.PoleA, Kierunki[jk])) //pionek
            {
                if (Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]) != 3 && kolor != Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[PoleControler.PoleC.NumerPola]))
                {
                    //szach
                    Console.WriteLine("Jest szach od pionka");
                    MozliweRuchy.ruchy = ruchyZapis;
                    Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
                    return false;
                }
            }
            //Console.WriteLine("Nie ma szacha");
            MozliweRuchy.ruchy = ruchyZapis;
            Chest_Server.Controlers.PoleControler.PoleA = poleKopiaA;
            return true;
        }
        public static void Rusz_sie_do(int b)
        {
            if (MozliweRuchy.ruchy.Contains(b))
            {
                string figuraPolaB = Chest_Server.Models.Szachownica.Plansza[b];
                Chest_Server.Models.Szachownica.Wykonanie_ruchu(PoleControler.PoleA.NumerPola, b);
                if (czyjRuch == 1) //biale
                {
                    if(Nie_ma_szacha(pozycjaKrolaBialego))
                    {
                        if(!Nie_ma_szacha(pozycjaKrolaCzarnego))
                        {

                        }
                    }
                    else
                    {
                        Chest_Server.Models.Szachownica.Wykonanie_ruchu(b, PoleControler.PoleA.NumerPola);
                        Chest_Server.Models.Szachownica.Plansza[b] = figuraPolaB;
                    }
                }
                else //czarne
                {
                    if(Nie_ma_szacha(pozycjaKrolaCzarnego))
                    {
                        if (!Nie_ma_szacha(pozycjaKrolaBialego))
                        {

                        }
                    }
                    else
                    {
                        Chest_Server.Models.Szachownica.Wykonanie_ruchu(b, PoleControler.PoleA.NumerPola);
                        Chest_Server.Models.Szachownica.Plansza[b] = figuraPolaB;
                    }
                }

                //czyszczenie POZNIEJ ZMIENIC (trzeba jescze sprawdzic czy nie ma szacha)
                PoleControler.PoleA = new Models.Pole();
                PoleControler.PoleC = new Models.Pole();
                //MozliweRuchy.ruchy = new List<int>();
                MozliweRuchy.ruchy.Clear();
            }
            else
                Console.WriteLine("Nie mozna wykonac takiego ruchu");
        }
        public static void Krol(int a)
        {
            for (int i = 0; i < 8; i++)
            {
                if (Czy_pole_istnieje(PoleControler.PoleA, Kierunki[i])) //zaczynay od pola u gory, ruch jak wskazowki zegara
                {
                    if (Czy_pole_jest_dozwolone())
                    {
                        MozliweRuchy.ruchy.Add(PoleControler.PoleC.NumerPola); //zapisuje numer pola, na ktore moge sie ruszyc, w liscie
                    }
                }
            }
        }
        public static void Hetman(int a)
        {
            for (int i = 0; i < 8; i++)
            {
                Rekurencja(a, i);
                sprawdzaczRekurencji = false;
            }
        }
        public static void Wieza(int a)
        {
            for (int i = 0; i < 8; i += 2)
            {
                Rekurencja(a, i);
                sprawdzaczRekurencji = false;
            }
        }
        public static void Goniec(int a)
        {
            for (int i = 1; i < 9; i += 2)
            {
                Rekurencja(a, i);
                sprawdzaczRekurencji = false;
            }
        }
        public static void Skoczek(int a)
        {
            for (int i = 8; i < 16; i++)
            {
                if (Czy_pole_istnieje(PoleControler.PoleA, Kierunki[i]))
                {
                    if (Czy_pole_jest_dozwolone())
                    {
                        MozliweRuchy.ruchy.Add(PoleControler.PoleC.NumerPola);
                    }
                }
            }
        }
        static bool Rekurencja(int ac, int i)
        {
            Pole polePomocnicze = new Pole();
            polePomocnicze.NumerPola = ac;
            polePomocnicze.x = Chest_Server.Models.Szachownica.Daj_kolumne(ac);
            polePomocnicze.y = Chest_Server.Models.Szachownica.Daj_wiersz(ac);
            Chest_Server.Controlers.PoleControler.PoleAC = polePomocnicze;
            if (Czy_pole_istnieje(PoleControler.PoleAC, Kierunki[i])) //zaczynay od pola u gory, ruch jak wskazowki zegara
            {
                if (Czy_pole_jest_dozwolone())
                {
                    MozliweRuchy.ruchy.Add(PoleControler.PoleC.NumerPola); //zapisuje numer pola, na ktore moge sie ruszyc, w liscie
                    if (sprawdzaczRekurencji)
                        return false;
                    Rekurencja(PoleControler.PoleC.NumerPola, i);

                }
            }
            return false;
        }

        public static void Pionek(int a, string figura)
        {
            int kolor;
            int hj;
            int jk;
            int kl;
            if (figura == "p") //1 - bialy, 0 - czarny
            {
                kolor = 0;
                hj = 8;
                jk = 56;
                kl = 8;
            }
            else
            {
                kolor = 1;
                hj = -8;
                jk = 0;
                kl = 48;
            }

            int NumerPolaC;
            int NumerPola1;
            int NumerPola2;
            NumerPolaC = Chest_Server.Controlers.PoleControler.PoleA.NumerPola + hj;
            NumerPola1 = Chest_Server.Controlers.PoleControler.PoleA.NumerPola + hj - 1;
            NumerPola2 = Chest_Server.Controlers.PoleControler.PoleA.NumerPola + hj + 1;

            if (!(Chest_Server.Controlers.PoleControler.PoleA.NumerPola % 8 == 0)) //czy jestem na lewej bandzie
            {
                if (Chest_Server.Models.Szachownica.Plansza[NumerPola1] != "*")
                {
                    if (kolor != Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[NumerPola1])) // Chest_Server.Models.Szachownica.Plansza[Chest_Server.Controlers.PoleControler.PoleA.NumerPola + 8 - 1]
                    {
                        MozliweRuchy.ruchy.Add(NumerPola1);
                        if (NumerPola1 >= jk && NumerPola1 <= jk + 7)
                        {
                            //Mozliwa przemiana
                            Console.WriteLine("Moge dokonac przemiany.");
                        }
                    }
                }

            }
            if (!(Chest_Server.Controlers.PoleControler.PoleA.NumerPola % 8 == 7)) //czy jestem na prawej bandzie?
            {
                if (Chest_Server.Models.Szachownica.Plansza[NumerPola2] != "*")
                {
                    if (kolor != Sprawdz_kolor(Chest_Server.Models.Szachownica.Plansza[NumerPola2])) // Chest_Server.Models.Szachownica.Plansza[Chest_Server.Controlers.PoleControler.PoleA.NumerPola + 8 - 1]
                    {
                        MozliweRuchy.ruchy.Add(NumerPola2);
                        if (NumerPola2 >= jk && NumerPola2 <= (jk + 7))
                        {
                            //Mozliwa przemiana
                            Console.WriteLine("Moge dokonac przemiany.");
                        }
                    }
                }
            }
            if (Chest_Server.Models.Szachownica.Plansza[NumerPolaC] == "*")
            {
                MozliweRuchy.ruchy.Add(NumerPolaC);
                if (NumerPolaC >= jk && NumerPolaC <= (jk + 7)) //0 7, 56 63
                {
                    //Mozliwa przemiana
                    Console.WriteLine("Moge dokonac przemiany.");
                }
                else
                {
                    if (NumerPolaC >= kl && NumerPolaC <= (kl + 7)) //48 55, 8 15
                    {

                    }
                    else
                    {
                        if (Chest_Server.Models.Szachownica.Plansza[NumerPolaC + hj] == "*")
                        {
                            MozliweRuchy.ruchy.Add(NumerPolaC + hj);
                        }
                    }
                }
            }
        }
        public static bool Czy_pole_istnieje(Chest_Server.Models.Pole A, Kierunek K)
        {
            int pomocniczyX = A.x + K.Wx;
            int pomocniczyY = A.y + K.Wy;
            if (pomocniczyX >= 1 && pomocniczyX <= 8 && pomocniczyY >= 1 && pomocniczyY <= 8)
            {
                Pole poleDoZapisu = new Pole();
                poleDoZapisu.x = pomocniczyX;
                poleDoZapisu.y = pomocniczyY;
                poleDoZapisu.NumerPola = A.NumerPola + K.Wskok;
                Chest_Server.Controlers.PoleControler.PoleC = poleDoZapisu;

                return true;
            }
            return false;
        }
        public static bool Czy_pole_jest_dozwolone()
        {
            int kolorA; //1 - bialy, 0 - czarny
            int kolorB;
            string FiguraPolaA = Szachownica.Plansza[PoleControler.PoleA.NumerPola]; //Chest_Server.Controlers.
            string FiguraPolaC = Szachownica.Plansza[PoleControler.PoleC.NumerPola]; //Chest_Server.Controlers.
            if (FiguraPolaC == "*")
            {
                return true; //true czyli mozna wykonac ruch
            }
            kolorA = Sprawdz_kolor(FiguraPolaA);
            kolorB = Sprawdz_kolor(FiguraPolaC);
            if (kolorA == kolorB)
                return false; //false czyli nie mozna wykonac ruchu
            else
            {
                sprawdzaczRekurencji = true; //mozna wykonac ruch poprzez zbicie, ale nie mozna isc dalej, wiec rekurencja stopuje
                return true;
            }
        }
        public static int Sprawdz_kolor(string FiguraPola)
        {
            if (FiguraPola == "*")
                return 3;
            if (FiguraPola == "K" || FiguraPola == "H" || FiguraPola == "W" || FiguraPola == "G" || FiguraPola == "S" || FiguraPola == "P")
                return 1;
            else
                return 0;

        }
    }
}
