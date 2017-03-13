using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeitmessung
{
    class Program
    {
        static void Main(string[] args)
        {
            Random würfel = new Random();
            List<int> werte = new List<int>();
            DateTime dtStart;
            DateTime dtStop;

            /*
            dtStart = DateTime.Now;
            for(int i = 0; i <= 10000; i++)
            {
                werte.Add(würfel.Next(1000));
            }
            dtStop = DateTime.Now;
            ausgabeZeitmessung(dtStart, dtStop, "Zeitmessung der for-Schleife");
            */

            // e: werte
            for (int j = 0; j <= 9; j++)
            {
                werte.Add(würfel.Next(100));
            }
            ausgabeListe(werte);
            blub(werte);
            ausgabeListe(werte);




            Console.ReadKey();
        }


        static void ausgabeZeitmessung(DateTime dt1, DateTime dt2, string text="")
        {
            TimeSpan diff = dt2 - dt1;
            Console.WriteLine("------------------------------------------------------------------");
            if(text != "")
                Console.WriteLine(text);
            Console.WriteLine(String.Format("Differenz von {0} - {1} der Messungen: "+diff, dt2, dt1));
            Console.WriteLine("------------------------------------------------------------------");
        }
        static void ausgabeListe(List<int> werte)
        {
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Ausgabe der Liste ");
            for(int i = 0; i < werte.Count; i++)
            {
                Console.WriteLine(String.Format("{0}: {1} ",i,werte[i]));
            }
            Console.WriteLine("------------------------------------------------------------------");
        }

        static void blub(List<int> werteListe)
        {
            Random würfel = new Random();
            List<int> werte = werteListe;
            DateTime dtStart;
            DateTime dtStop;

            int i = 0;
            while (i < werte.Count)
            {
                int j = werte.Count - 1;
                while( j > 0)
                {
                    if(werte[j] < werte[j - 1])
                    {
                        int tausch = werte[j];
                        werte[j] = werte[j-1];
                        werte[j-1] = tausch;
                     }
                    j--;
                }
                i++;
            }
        }
    }
}
