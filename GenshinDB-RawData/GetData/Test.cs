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
            
            

            /*HttpClient client = new HttpClient();
            FileStream filestream = File.OpenWrite("D:\\GenshinDB\\GenshinDB-RawData\\RawDataFiles(txt)\\wikimaterial.txt");
            StreamWriter writer = new StreamWriter(filestream);
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Character_Ascension_Materials").Result + "\n");
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Character_EXP_Materials").Result + "\n");
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Common_Ascension_Materials").Result + "\n");
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Refinement_Materials").Result + "\n");
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Talent_Level-Up_Materials").Result + "\n");
            writer.Write(client.GetStringAsync("https://genshin-impact.fandom.com/wiki/Weapon_Ascension_Materials").Result + "\n");

            writer.Close();

            string text = File.ReadAllText("D:\\GenshinDB\\GenshinDB-RawData\\RawDataFiles(txt)\\wikimaterial.txt");
            MatchCollection tempMatchCollection = Regex.Matches(text, "<td><div class=\"card_with_caption hidden\".*?\n</td>");
            List<string> names = new List<string>();
            foreach (Match line in tempMatchCollection)
            {
                Match name = Regex.Match(line.Value, "title=\".*?\"");
                while(true)
                {
                    name = name.NextMatch();
                    if (name.Value == "") break;
                    if (!names.Contains(name.Value)) names.Add(name.Value); 
                }
            }
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }
            */
        }
    }
}
