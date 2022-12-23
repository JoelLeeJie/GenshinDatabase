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
        Character tempc;
        MatchCollection tempMatch;
        List<Character> characterData;
        string text;
        static void Main()
        {
            Test test = new Test();
            test.characterData = new List<Character>();
            test.testing();
        }
        void testing()
        {
            tempc = new Character();
            text = File.ReadAllText("D:\\GenshinDB\\GenshinDB-RawData\\RawDataFiles(txt)\\Characters\\Albedo.txt");
            tempMatch = Regex.Matches(text, "unlock\":.*?\"description\":\".*?\"");
            Console.WriteLine(Regex.Matches(tempMatch[6].Value, ":\".*?\"")[1].Value.Trim(':')); ;
        }
    }
}
