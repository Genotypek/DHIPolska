using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// Program generuje tablicę dwuwymiarową o zadanych wymiarach, wypełnioną losowymi dodatnimi liczbami całkowitymi i zapisuje ją do pliku tekstowego.
// Wczytuje zapisany plik ponownie do programu.
// Oblicza średnią z liczb w każdej z kolumn.
// Do nowego pliku tekstowego zapisuje wczytaną tablicę z kolumnami posortowanymi według rosnącej średniej obliczonej w punkcie 3.

namespace ZadanieDomowe
{
    class Program
    {
        internal static string SciezkaZapisuTablicy = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\tablica.txt";
        internal static string SciezkaZapisuPosortowanejTablicy = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\posortowana tablica.txt";

        static void Main(string[] args)
        {
            int[,] tablica = UtworzTablice2d();
            WypelnijTablice2d(tablica);

            Console.WriteLine("Wypełniona tablica wygląda tak:");

            PokazTablice2d(tablica);
            ZapiszTablice2dDoPliku(tablica, SciezkaZapisuTablicy);
            int[,] tablica2 = WczytajTablice2dZPliku(SciezkaZapisuTablicy);

            Console.WriteLine("\nWczytana tablica z pliku wygląda tak:");
            PokazTablice2d(tablica2);

            float[] srednia = LiczSredniaKolumn(tablica2);

            Console.WriteLine("Wyliczone średnie kolumn to kolejno:");
            PokazTablice1d(srednia);

            SortujKolumnyPoKluczu(tablica2, srednia);

            Array.Sort(srednia);
            Console.WriteLine("\nPosorwoane średnie kolumn to kolejno:");
            PokazTablice1d(srednia);

            Console.WriteLine("\nPosortowane kolumny tablicy wg. średniej wyglądają tak:");
            PokazTablice2d(tablica2);

            ZapiszTablice2dDoPliku(tablica2, SciezkaZapisuPosortowanejTablicy);

            Console.WriteLine("\nProgram wykonany na potrzeby rekrutacji dla firmy DHI Polska.");
            Console.WriteLine("Autor: Styś Mateusz");
            Console.ReadKey();
        }

        /// <summary>
        /// Sortowanie kolumn dwuwymiarowej tablicy na podstawie klucza.
        /// </summary>
        /// <param name="tablica">Dwuwymiarowa tablica do posortowania kolumn.</param>
        /// <param name="klucz">Klucz na podstawie którego tablica ma zostać posortowana.</param>
        private static void SortujKolumnyPoKluczu(int[,] tablica, float[] klucz)
        {
            int[] pomocnicza = new int[tablica.GetLength(1)];
            for (int wiersz = 0; wiersz < tablica.GetLength(0); wiersz++)
            {
                // Tworzę tablicę tymczasową dla klucza, gdyż jest on także sortowany.
                float[] kluczTemp = (float[]) klucz.Clone();

                // Wypełniam tablice pomocniczą.
                for (int kolumna = 0; kolumna < tablica.GetLength(1); kolumna++)
                {
                    pomocnicza[kolumna] = tablica[wiersz, kolumna];
                }

                Array.Sort(kluczTemp, pomocnicza);

                // Zamieniam wartości w tablicy głównej.
                for (int kolumna = 0; kolumna < tablica.GetLength(1); kolumna++)
                {
                    tablica[wiersz, kolumna] = pomocnicza[kolumna];
                }
            }
        }

        /// <summary>
        /// Zapisuje tablicę jednowymiarową do pliku.
        /// </summary>
        /// <param name="tablica">Tablica jednowymiarowa, która ma zostać zapisana do pliku.</param>
        /// <param name="plik">Ścieżka pliku, w którym tablica ma zostać zapisana.</param>
        //private static void ZapiszTablice1dDoPliku(float[] tablica, string plik)
        //{
        //    Console.WriteLine("\nZapisywanie tablicy do pliku: " + plik);
        //    List<string> LinieDoZapisania = new List<string>();

        //    foreach (var element in tablica)
        //    {
        //        LinieDoZapisania.Add(element.ToString());
        //    }

        //    System.IO.File.WriteAllLines(plik, LinieDoZapisania);
        //}

        /// <summary>
        /// Wyświetla jednowymiarową tablicę w konsoli.
        /// </summary>
        /// <param name="tablica">Tablica jednowymiarowa, która ma zostać wyświetlona w konsoli.</param>
        private static void PokazTablice1d(float[] tablica)
        {
            foreach (var element in tablica)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Liczy średnią każdej kolumny w tablicy dwuwymiarowej.
        /// </summary>
        /// <param name="tablica">Tablica dwuwymiarowa, której kolumn średnia ma być liczona.</param>
        /// <returns></returns>
        private static float[] LiczSredniaKolumn(int[,] tablica)
        {
            float[] srednia = new float[tablica.GetLength(1)];
            for (int kolumna = 0; kolumna < tablica.GetLength(1); kolumna++)
            {
                int sum = 0;

                // sumowanie wartości kolumn.
                for (int wiersz = 0; wiersz < tablica.GetLength(0); wiersz++)
                {
                    sum += tablica[wiersz, kolumna];
                }

                // Obliczanie średniej kolumn i umieszczanie w jednowymiarowej tablicy.
                srednia[kolumna] = (float)sum / tablica.GetLength(0);
            }

            return srednia;
        }

        /// <summary>
        /// Wyświetla dwuwymiarową tablicę w konsoli.
        /// </summary>
        /// <param name="tablica">Tablica dwuwymiarowa, która ma zostać wyświetlona.</param>
        private static void PokazTablice2d(int[,] tablica)
        {
            int kolumna = 0;
            foreach (var element in tablica)
            {
                kolumna += 1;
                Console.Write(element + " ");
                if (kolumna == tablica.GetLength(1))
                {
                    Console.WriteLine();
                    kolumna = 0;
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Wczytuje dwuwymiarową tablicę z wcześniej utworzonego pliku.
        /// </summary>
        /// <param name="plik">Ścieżka pliku, z którego tablica ma zostać wczytana.</param>
        /// <returns>Wczytana dwuwymiarowa tablica.</returns>
        private static int[,] WczytajTablice2dZPliku(string plik)
        {
            Console.WriteLine("Wczytywanie tablicy z pliku: " + plik);
            List<string> LinieDoWczytania = new List<string>();

            LinieDoWczytania = File.ReadAllLines(plik).ToList();

            // Tworzenie nowej tablicy o odpowiedniej wielkości
            int[,] tablica = new int[LinieDoWczytania.Capacity, LinieDoWczytania[0].Count(Char.IsWhiteSpace)];

            // Przenoszenie listy stringów do tablicy int'ów
            for (int wiersz = 0; wiersz < LinieDoWczytania.Capacity; wiersz++)
            {
                int kolumna = 0;

                // Usuwam niepotrzebną spację z końca, by nie stworzyć więcej elementów niż potrzeba
                LinieDoWczytania[wiersz] = LinieDoWczytania[wiersz].Trim();
                string[] elementy = LinieDoWczytania[wiersz].Split(' ');
                
                // Wypełniam nową tablicę elementami
                foreach (string element in elementy)
                {
                    tablica[wiersz, kolumna] = int.Parse(element);
                    kolumna += 1;
                }
            }

            return tablica;
        }

        /// <summary>
        /// Zapisuje dwuwymiarową tablicę do pliku.
        /// </summary>
        /// <param name="tablica">Dwuwymiarowa tablica, która ma zostać zapisana do pliku</param>
        /// <param name="plik">Ścieżka pliku, w którym tablica ma zostać wczytana.</param>
        private static void ZapiszTablice2dDoPliku(int[,] tablica, string plik)
        {
            Console.WriteLine("Zapisywanie tablicy do pliku: " + plik);
            List<string> LinieDoZapisania = new List<string>();

            // Wypełnianie listy wierszami tablicy
            for (int wiersz = 0; wiersz < tablica.GetLength(0); wiersz++)
            {
                string linia = "";
                for (int kolumna = 0; kolumna < tablica.GetLength(1); kolumna++)
                    linia += tablica[wiersz, kolumna] + " ";
                LinieDoZapisania.Add(linia);
            }

            // Zapisanie na pulpit
            File.WriteAllLines(plik, LinieDoZapisania);
        }

        /// <summary>
        /// Wypełnia tablicę losowymi dodatnimi liczbami całkowitymi
        /// </summary>
        /// <param name="tablica">Dwuwymiarowa tablica, która będzie zapełniana.</param>
        private static void WypelnijTablice2d(int[,] tablica)
        {
            Random losowa = new Random();

            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                for (int j = 0; j < tablica.GetLength(1); j++)
                {
                    tablica[i, j] = losowa.Next(1001); // Wypełniam tablicę losowymi liczbami z zakresu 0-1000
                }
            }
        }

        /// <summary>
        /// Tworzy tablicę o rozmiarach podanych przez użytkownika w konsoli
        /// </summary>
        private static int[,] UtworzTablice2d()
        {
            Console.WriteLine("Program stworzy teraz tablicę o podanych przez Ciebie wymiarach.");
            Console.Write("Podaj ilość wierszy tablicy: ");

            int wiersze;

            // Zabezpieczam się przed podaniem ujemnej ilości wierszy oraz innej wartości niż liczbowej
            while (true) {
                if (int.TryParse(Console.ReadLine(), out wiersze) && wiersze > 0)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wprowadzono niepoprawną wartość!");
                    Console.Write("Podaj ilość wierszy tablicy: ");
                }
            }
            Console.WriteLine("Zdecydowałeś, że tablica będzie posiadać " + wiersze + " wierszy.");
            Console.Write("Podaj ilość kolumn tablicy: ");

            int kolumny;

            // Zabezpieczam się przed podaniem ujemnej ilości kolumn oraz innej wartości niż liczbowej
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out kolumny) && kolumny > 0)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wprowadzono niepoprawną wartość!");
                    Console.Write("Podaj ilość kolumn tablicy: ");
                }
            }
            Console.WriteLine("Zdecydowałeś, że tablica będzie posiadać " + wiersze + " wierszy.");
            Console.WriteLine("Zdecydowałeś, że tablica będzie posiadać " + kolumny + " kolumn.\n");

            int[,] tablica = new int[wiersze, kolumny];

            return tablica;
        }
    }
}
