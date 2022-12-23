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
        }
        internal void CharacterRawData() //writes raw data to struct form
        {
            int counter = 1;
            List<string> fileNames = Directory.GetFiles(characterRawDataPath, "*.txt").ToList<string>();
            foreach(string path in fileNames)
            {
                tempc.talents = new List<string>() { };
                tempc.constellation = new List<string>() { }; //need to make the list empty again for the next character.
                try
                {
                    string text = File.ReadAllText(path);
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
                catch(Exception) { Console.WriteLine(tempc.name + ": error convert to struct"); continue; }
                tempc.id = counter++;
                characterData.Add(tempc); //since tempc is a struct, changing it won't affect previous copies of it in the list. This doesn't apply for reference variables in a struct.
                Console.WriteLine(tempc.name + ": success in converting to struct");
            }


        }

        internal void ArtifactRawData()
        {

        }

        internal void WeaponRawData()
        {

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

        internal int hp { get; set; }
        internal int atk { get; set; }
        internal int def { get; set; }
        internal int em { get; set; }
        internal int cd { get; set; }
        internal int cr { get; set; }
        internal int er { get; set; }
        internal int dmg_bonus { get; set; }
        internal int levelid { get; set; }
    }

    internal struct Artifact
    {

    }

    internal struct Weapon
    {

    }
}
