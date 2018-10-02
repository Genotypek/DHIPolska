using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZadanieDomowe
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] tablica = UtworzTablice2d();
            WypelnijTablice2d(tablica);

            Console.WriteLine("Wypełniona tablica wygląda tak:");

            PokazTablice2d(tablica);
            ZapiszTablice2dDoPliku(tablica);
            int[,] tablica2 = WczytajTablice2dZPliku();

            Console.WriteLine("\nWczytana tablica z pliku wygląda tak:");
            PokazTablice2d(tablica2);

            int[] srednia = LiczSredniaKolumn(tablica2);
        }

        private static int[] LiczSredniaKolumn(int[,] tablica2)
        {
            
        }

        /// <summary>
        /// Wyświetla przekazaną dwuwymiarową tablicę w konsoli
        /// </summary>
        /// <param name="tablica">Tablica dwuwymiarowa, która ma zostać wyświetlona</param>
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
        /// Wczytuje tablicę z wcześniej utworzonego pliku
        /// </summary>
        /// <returns></returns>
        private static int[,] WczytajTablice2dZPliku()
        {
            Console.WriteLine("Wczytywanie tablicy z pliku: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.txt");
            List<string> LinieDoWczytania = new List<string>();

            LinieDoWczytania = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.txt").ToList();

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
        /// Konwertuje tablicę wielowymiarową na listę stringów i zapisuje ją do pliku na pulpicie aktywnego użytkownika
        /// </summary>
        /// <param name="tablica">Dwuwymiarowa tablica, która ma zostać zapisana do pliku</param>
        private static void ZapiszTablice2dDoPliku(int[,] tablica)
        {
            Console.WriteLine("Zapisywanie tablicy do pliku: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.txt");
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
            File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\test.txt", LinieDoZapisania);
        }

        /// <summary>
        /// Wypełnia tablicę losowymi dodatnimi liczbami całkowitymi
        /// </summary>
        /// <param name="tablica">Dwuwymiarowa tablica, która będzie zapełniana.</param>
        private static void WypelnijTablice2d(int[,] tablica)
        {
            // Tworzę nowy obiekt Random
            Random losowa = new Random();
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                for (int j = 0; j < tablica.GetLength(1); j++)
                {
                    tablica[i, j] = losowa.Next(1001); // Wypełnia tablicę losowymi liczbami z zakresu 0-1000
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
