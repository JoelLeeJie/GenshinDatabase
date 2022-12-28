using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinDB;
using System.Text.RegularExpressions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace RawData
{
    class Test
    {
        static void Main()
        {
            string temp = ""; string input; string[] arrays = new string[8];
            for(int i = 0; i < arrays.Length; i++)
            {
                arrays[i] = Console.ReadLine();
            }
            for(int id = 42; id<46; id++)
            {
                foreach(string s in arrays)
                {
                    temp = temp + id + "," + s + "\n";
                }
            }
            File.WriteAllText("D:\\GenshinDB\\GenshinDB-RawData\\CsvFIles(CSV)\\traveler.txt", temp);

        }
    }
}


