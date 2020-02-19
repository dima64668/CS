using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsLab1Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Analysis(Directory.GetCurrentDirectory() + @"\texts\t1.txt");
            Console.WriteLine();
            Console.WriteLine();
            Analysis(Directory.GetCurrentDirectory() + @"\texts\t2.txt");
            Console.WriteLine();
            Console.WriteLine();
            Analysis(Directory.GetCurrentDirectory() + @"\texts\t3.txt");
            Console.ReadKey();
            //
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t1_base64.txt");
            //Console.WriteLine();
            //Console.WriteLine();
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t2_base64.txt");
            //Console.WriteLine();
            //Console.WriteLine();
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t3_base64.txt");
            //Console.ReadKey();
            //
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t1_base64.bz2");
            //Console.WriteLine();
            //Console.WriteLine();
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t2_base64.bz2");
            //Console.WriteLine();
            //Console.WriteLine();
            //Analysis(Directory.GetCurrentDirectory() + @"\texts\t3_base64.bz2");
            //Console.ReadKey();
            //
        }

        static void Analysis(string path)
        {
            Console.WriteLine("  " + path);
            FileInfo fi = new FileInfo(path);

            double total = 0;
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                int i;
                while ((i = sr.Read()) > -1)
                {
                    if (!dictionary.ContainsKey(Convert.ToChar(i)))
                    {
                        dictionary.Add(Convert.ToChar(i), 1);
                    }
                    else
                    {
                        dictionary[Convert.ToChar(i)] += 1;
                    }
                    total++;
                }
            }

            double frequency = 0;
            double entropy = 0;
            double total_entropy = 0;
            foreach (KeyValuePair<char, int> i in dictionary)
            {
                frequency = i.Value / total;
                entropy = frequency * Math.Log(1 / frequency, 2);
                total_entropy += entropy;
                Console.WriteLine("\t{0} - частота: {1:f8}, ентропія: {2:f8}", i.Key, frequency, entropy);
                entropy = 0;
                frequency = 0;
            }
            Console.WriteLine("\tЗагальна ентропія: {0:f8}", total_entropy);
            Console.WriteLine("\tКількість інформації: {0:f8} byte, розмір файлу: {1} byte", (total * total_entropy)/8, fi.Length);
        }

    }
}
