using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace GenshinDB
{
    internal class ConvertRawData
    {
        string rawDataFilePath;
        string characterRawDataPath;
        string artifactRawDataPath;
        string weaponRawDataPath;

        internal List<Character> characterData;
        internal List<Artifact> artifactData;
        internal List<Weapon> weaponData;

        internal List<CharacterStat> characterStatData;
        internal List<WeaponStat> weaponStatData;

        Character tempc;
        Artifact tempa;
        Weapon tempw;
        MatchCollection tempMatch;


        internal ConvertRawData(string filePath)
        {
            rawDataFilePath = Directory.GetParent(filePath).FullName + "\\RawDataFiles(txt)";
            characterRawDataPath = rawDataFilePath + "\\Characters";
            artifactRawDataPath = rawDataFilePath + "\\Artifacts";
            weaponRawDataPath = rawDataFilePath + "\\Weapons";


            tempc = new Character();
            tempa = new Artifact();
            tempw = new Weapon();
            MatchCollection tempMatch;


            characterData = new List<Character>();
            artifactData = new List<Artifact>();
            weaponData = new List<Weapon>();

            characterStatData = new List<CharacterStat>();
            weaponStatData = new List<WeaponStat>();
        }
        internal void CharacterRawData() //writes raw data to struct form
        {
            int counter = 1;
            List<string> fileNames = Directory.GetFiles(characterRawDataPath, "*.txt").ToList<string>();
            foreach(string path in fileNames)
            {
                tempc.talents = new List<string>() { };
                tempc.constellation = new List<string>() { }; //need to make the list empty again for the next character. Also dereferences it from the existing tempc.talent/constellation to prevent overwriting.
                try
                {
                    string text = File.ReadAllText(path);
                    text = text.Replace("\\\"", "'"); //to deal with \".
                    tempc.name = Regex.Match(text, "\"name\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
                    tempc.element = Regex.Match(text, "\"vision\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
                    tempc.region = Regex.Match(text, "\"nation\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
                    tempc.weapontype = Regex.Match(text, "\"weapon\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"');
                    tempc.rarity = int.Parse(Regex.Match(text, "\"rarity\":.*?,").Value.Split(':')[1].Trim(' ', ',', '\"'));
                    tempc.description = Regex.Match(text, "\"description\":.*?,\"").Value.Split(':')[1].Trim(' ', ',', '\"', '\\'); //stops at ," instead of , to prevent extra commas from interfering.

                    tempMatch = Regex.Matches(text, "unlock\":.*?\"description\":\".*?\"");
                    for (int i = 0; i < tempMatch.Count(); i++)
                    { 
                        if (i < 3) tempc.talents.Add(Regex.Matches(tempMatch[i].Value, ":\".*?\"")[1].Value.Trim(':', '\"').Replace("\\n", " ")); //since description of constellation may include ":" which shouldnt be split.
                        if (i > tempMatch.Count()-7) tempc.constellation.Add(Regex.Matches(tempMatch[i].Value, ":\".*?\"")[1].Value.Trim(':', '\"').Replace("\\n", " "));
                    }
                }
                catch(NullReferenceException) { Console.WriteLine(tempc.name + ": Null reference when converting to struct"); continue; }
                catch(Exception) { Console.WriteLine(tempc.name + ": character error convert to struct"); continue; }
                tempc.id = counter++;
                characterData.Add(tempc); //since tempc is a struct, changing it won't affect previous copies of it in the list. This doesn't apply for reference variables in a struct.
            }


        }

        internal void ArtifactRawData()
        {
            int counter = 1;
            List<string> fileNames = Directory.GetFiles(artifactRawDataPath, "*.txt").ToList<string>();
            foreach(string path in fileNames)
            {
                tempa = new Artifact();
                try
                {
                    string text = File.ReadAllText(path);
                    text = text.Replace("\\\"", "'");
                    tempa.name = Regex.Match(text, "name\":\".*?\",").Value.Split(':')[1].Trim('"', ' ', ',');
                    if(Regex.Match(text, "1-piece_bonus\":\".*?\"")?.Value != "")
                        tempa.onepiece = Regex.Match(text, "1-piece_bonus\":\".*?\"").Value.Split(':')[1].Trim('"', ' '); //need ? to check for null, to prevent accessing [1] for null(index out of range).
                    if(Regex.Match(text, "2-piece_bonus\":\".*?\",")?.Value != "")
                        tempa.twopiece = Regex.Match(text, "2-piece_bonus\":\".*?\",").Value.Split(':')[1].Trim('"', ' ', ',');
                    if (Regex.Match(text, "4-piece_bonus\":\".*?\"")?.Value != "")
                        tempa.fourpiece = Regex.Match(text, "4-piece_bonus\":\".*?\"").Value.Split(':')[1].Trim('"', ' ');
                }
                catch(Exception)
                {
                    Console.WriteLine(tempa.name + ": artifact error convert to struct");
                    continue;
                }
                tempa.id = counter++;
                artifactData.Add(tempa);
            }

        }

        internal void WeaponRawData()
        {
            int counter = 1;
            List<string> fileNames = Directory.GetFiles(weaponRawDataPath, "*.txt").ToList<string>();
            foreach (string path in fileNames)
            {
                tempw = new Weapon();
                tempw.refinements = new List<string>();
                try
                {
                    string text = File.ReadAllText(path);
                    text.Replace("\\\"", "'"); //to replace \" with '
                    tempw.name = Regex.Match(text, "\"name\":.*?\",").Value.Split(':')[1].Trim('"', ',', ' ');
                    tempw.weapontype = Regex.Match(text, "\"type\":.*?\",").Value.Split(':')[1].Trim('"', ',', ' ');
                    tempw.rarity = int.Parse(Regex.Match(text, "\"rarity\":.*?,").Value.Split(':')[1].Trim('"', ',', ' '));
                    tempw.refinementData = Regex.Match(Regex.Match(text, "\"passiveDesc\":.*?\",").Value, ":\".*?\"").Value.Trim('"', ',', ' ', ':');
                }
                catch(Exception) { Console.WriteLine(tempw.name + ": weapon error convert to struct"); continue; }
                tempw.id = counter++;

                //turn refinementData into 5 separate statements to put into refinements.
                tempMatch = Regex.Matches(tempw.refinementData, @"[\d\%\.]*?/.+?/.+?/.+?/.*?[\s]");
                int offset = 0;
                foreach (Match m in tempMatch)
                {
                    tempw.refinementData = tempw.refinementData.Remove(m.Index - offset, m.Length); //when replacing the first m, the string gets shorter, the second m index isn't accurate anymore.
                    offset += m.Length;
                }
                for (int i = 0; i < 5; i++) //5 refinements.
                {
                    string replacedtext = tempw.refinementData;
                    offset = 0;
                    foreach (Match m in tempMatch)
                    {
                        replacedtext = replacedtext.Insert(m.Index - offset, m.Value.Trim(' ', '.').Split('/')[i] + " ");
                        offset += m.Length - (m.Value.Trim(' ', '.').Split('/')[i] + " ").Length;
                    }
                    tempw.refinements.Add(replacedtext.Replace("  ", ". ")); //replace any lost '.'
                }
                weaponData.Add(tempw);
            }
        }

        CharacterStat tempcs;
        WeaponStat tempws;
        internal void CharacterStatRawData()
        {
            List<string> fileNamePaths = Directory.GetFiles(rawDataFilePath + "\\CharacterWiki").ToList<string>();
            string temp; Match tempMatch; MatchCollection tempMatchCollection;
            int counter = 1;
            string[] level_levelarray = new string[] { "1/20", "20/20", "40/40", "50/50", "60/60", "70/70", "80/80", "90/90" };
            foreach(string path in fileNamePaths)
            {
                temp = File.ReadAllText(path);

                try
                {
                    for (int i = 0; i < level_levelarray.Length; i++)
                    {
                        filterData(level_levelarray[i], int.Parse(level_levelarray[i].Split('/')[0]));
                    }
                    //1/20. 20/20, 40/40 etc for stats(for every level, 
                    //20/40, 40/50 to find ascension stat(remember to link it to the right levelid)
                }
                catch(Exception)
                {
                    Console.WriteLine(counter + ": error convert charstat to csv");
                    continue;
                }
            }

            void filterData(string level_level, int level)
            {
                tempcs = new CharacterStat();
                tempcs.charid = counter++;
                tempcs.levelid = level;
                tempMatch = Regex.Match(temp, $"{level_level}</td><td>.*?</td><td>.*?</td><td>.*?</td>");
                tempMatch = Regex.Match(tempMatch.Value, "<td>.*?</td>"); //find the values of hp,atk,def in sequence.
                tempcs.hp = int.Parse(tempMatch.Value);
                tempcs.atk = int.Parse(tempMatch.NextMatch().Value);
                tempcs.def = int.Parse(tempMatch.NextMatch().Value);
                tempMatch = Regex.Match(temp, "Ascension.*?Phase.*?Level.*?Special Stat.*?title.*?\".*?\"");
                tempMatch = Regex.Match(tempMatch.Value, "title.*?\".*?\"");
                tempMatch = Regex.Match(tempMatch.Value, "\".*?\"");
                tempcs.ascensionname = CheckStatName(tempMatch.Value.Trim('"'));
                if (level_level != "1/20")
                {
                    tempMatch = Regex.Match(temp, $"td rowspan=\"2\".*?{level_level}"); //finds the ascension value for each ascension level
                    tempMatch = Regex.Match(tempMatch.Value, ">.*?</td>");
                    if (tempMatch.Value.Replace("</td>", "").Trim('<', ' ') == "&#8212") tempcs.ascensionvalue = 0;
                    else tempcs.ascensionvalue = int.Parse(tempMatch.Value.Replace("</td>", "").Trim('<', ' '));
                }
                else tempcs.ascensionvalue = 0;
                characterStatData.Add(tempcs);
            }

            string CheckStatName(string input)
            {
                switch(input.ToLower())
                {
                    case "geo dmg bonus": return "geo dmg%";
                    case "anemo dmg bonus": return "anemo dmg%";
                    case "hydro dmg bonus": return "hydro dmg%";
                    case "pyro dmg bonus": return "pyro dmg%";
                    case "dendro dmg bonus": return "dendro dmg%";
                    case "cryo dmg bonus": return "cryo dmg%";
                    case "electro dmg bonus": return "electro dmg%";
                    case "physical dmg bonus": return "phy dmg%";
                    case "atk": return "atk%";
                    case "hp": return "hp%";
                    case "def": return "def%";
                    case "energy recharge": return "er%";
                    case "elemental mastery": return "em";
                    case "healing bonus": return "healing bonus%";
                    case "crit dmg": return "crit dmg%";
                    case "crit rate": return "crit rate%";
                    default: throw new Exception("no ascension stat name match");
                }
            }
        }

        internal void WeaponStatRawData()
        {
            List<string> fileNamePaths = Directory.GetFiles(rawDataFilePath + "\\WeaponWiki").ToList<string>();
            string temp;
            int counter = 1;
            foreach (string path in fileNamePaths)
            {
                temp = File.ReadAllText(path);
                tempws = new WeaponStat();
            }

        }
    }

    internal struct Character
    {
        internal int id{get; set;}
        internal string name{get; set;}
        internal string weapontype { get; set; }
        internal string region { get; set; }
        internal int rarity { get; set; }
        internal string element { get; set; }
        internal string description { get; set; }
        internal List<string> talents;

        internal List<string> constellation; //don't use a reference variable in a struct!
    }

    internal struct Artifact
    {
        internal int id { get; set; }
        internal string name { get; set; }
        internal string onepiece { get; set; }
        internal string twopiece { get; set; }
        internal string fourpiece { get; set; }

    }

    internal struct Weapon
    {
        //id, name, weapon_type, rarity, r1-r5 refinement description, 
        internal int id { get; set; }
        internal string name;
        internal string weapontype;
        internal int rarity { get; set; }
        internal string refinementData;
        internal List<string> refinements;
    }

    internal struct WeaponStat
    {
        internal int weaponid { get; set; }
        internal int levelid { get; set; }
        internal int atk { get; set; }
        internal string statname { get; set; }
        internal float statvalue { get; set; }
    }

    internal struct CharacterStat
    {
        internal int charid { get; set; }
        internal int levelid { get; set; }
        internal int hp { get; set; }
        internal int atk { get; set; }
        internal int def { get; set; }
        internal string ascensionname { get; set; }
        internal float ascensionvalue { get; set; }
    }

}
