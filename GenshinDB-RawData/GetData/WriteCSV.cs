using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GenshinDB
{
    internal class WriteCSV
    {
        string csvFolderPath;

        internal List<Character> characterData;
        internal List<Artifact> artifactData;
        internal List<Weapon> weaponData;


        internal WriteCSV(List<Character> cData, List<Artifact> aData, List<Weapon> wData, string filePath)
        {
            characterData = cData;
            artifactData = aData;
            weaponData = wData;
            csvFolderPath = Directory.GetParent(filePath).FullName + "\\CsvFIles(CSV)";
        }

        internal void WriteAllCSVFiles() //starts all functions
        {
            WriteCharacterInfo(); // table: characterinfo
            WriteCharacterConstellations(); // table: characterconstellations
        }

        void WriteCharacterInfo()
        {
            string path = csvFolderPath + "\\characterinfo.csv";
            string temp = "id,name,description,element,weapon_type,region,rarity,normalattack,elementalskill,elementalburst\n";
            foreach(Character c in characterData)
            {
                try
                {
                    temp = temp + c.id + "," + c.name + "," + "\"" + c.description + "\"" + "," + c.element + "," + c.weapontype + "," + c.region + "," + c.rarity + ",\"" + c.talents[0] + "\",\"" + c.talents[1] + "\",\"" + c.talents[2] + "\"\n";
                    temp = temp.Replace("\\\"", "'"); //replaces \" with '. Don't replace \n here as it'll intefere with the new \n.
                } //"" around description to ignore extra commas when copying.
                catch (Exception)
                {
                    Console.WriteLine(c.name + ": write csv cinfo file");
                    continue;
                }
            }
            File.WriteAllText(path, temp);
        }

        void WriteCharacterConstellations()
        {
            string path = csvFolderPath + "\\characterconstellations.csv";
            string temp = "charid,c1,c2,c3,c4,c5,c6\n";
            foreach(Character c in characterData)
            {
                try
                {
                    temp = temp + c.id;
                    foreach (string constellation in c.constellation)
                    {
                        temp = temp + ",\"" + constellation + "\"";
                    }
                    temp = temp + "\n";
                }
                catch (Exception)
                {
                    Console.WriteLine(c.name + ": error writing csv cconstellation file");
                    continue;
                }
            }
            File.WriteAllText(path, temp);
        }
    }
}
