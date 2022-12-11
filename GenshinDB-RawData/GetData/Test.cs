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
    internal class Test
    {
        string text;
        Character amber;
        List<string> fileNames;
        int counter = 0;
        Character tempc;
        List<Character> characterData;

        internal Test()
        {
            characterData = new List<Character>();
        }
        static void Main()
        {
            Test test = new Test();

            test.CharacterFilterText("D:\\GenshinDB\\GenshinDB-RawData\\RawDataFiles(txt)\\Characters\\amber.txt");
            
        }

        void CharacterFilterText(string filePath)
        {
            text = File.ReadAllText(filePath);
            amber.name = Regex.Match(text, "\"name\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
            amber.element = Regex.Match(text, "\"vision\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
            amber.region = Regex.Match(text, "\"nation\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
            amber.weapontype = Regex.Match(text, "\"weapon\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
            amber.rarity = int.Parse(Regex.Match(text, "\"rarity\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"'));

            Console.WriteLine(amber.name);
            Console.WriteLine(amber.element);
            Console.WriteLine(amber.region);
            Console.WriteLine(amber.weapontype);
            Console.WriteLine(amber.rarity);
        }
    }
}
