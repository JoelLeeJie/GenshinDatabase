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
            text = "Every 4s a character is on the field, their ATK increases by 4/5/6/7/8% and their CRIT DMG increases by 4/5/6/7/8%. This effect has a maximum of 5 stacks and will not be reset if the character leaves the field, but will be cleared when the character takes DMG.";
            tempMatch = Regex.Matches(text, @"[\d\%]*?/.+?/.+?/.+?/.*?[\s\.]");
            int offset = 0;
            foreach (Match m in tempMatch)
            {
                text = text.Remove(m.Index - offset, m.Length); //when replacing the first m, the string gets shorter, the second m index isn't accurate anymore.
                offset += m.Length;
            }
            for(int i = 0; i<4; i++) //5 refinements.
            {
                string replacedtext = text;
                offset = 0;
                foreach (Match m in tempMatch)
                {
                    replacedtext = replacedtext.Insert(m.Index-offset, m.Value.Trim(' ', '.').Split('/')[i]+" ");
                    offset += m.Length - (m.Value.Trim(' ', '.').Split('/')[i]+ " ").Length;
                }
                Console.WriteLine(replacedtext.Replace("  ", ". "));
            }
            
        }
    }
}
