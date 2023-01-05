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
            Test test = new Test();
            test.Testing();
        }
        void Testing()
        {
            string temp = "hi\rbye\rtry";
            Match tempMatch = Regex.Match(temp, "hi[a-zA-Z/\r/]*?try");
            Console.WriteLine($@"{tempMatch.Value.Replace("\r", "")}");
            Console.WriteLine(temp);
        }
    }
}


