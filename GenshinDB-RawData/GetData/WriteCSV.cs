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

        internal List<CharacterStat> characterStatData;
        internal List<WeaponStat> weaponStatData;


        internal WriteCSV(List<Character> cData, List<Artifact> aData, List<Weapon> wData, List<CharacterStat> csData, List<WeaponStat> wsData, string filePath)
        {
            characterData = cData;
            artifactData = aData;
            weaponData = wData;

            characterStatData = csData;
            weaponStatData = wsData;

            csvFolderPath = Directory.GetParent(filePath).FullName + "\\CsvFIles(CSV)";
        }

        internal void WriteAllCSVFiles() //starts all functions
        {
            WriteCharacterInfo(); // table: characterinfo
            WriteCharacterConstellations(); // table: characterconstellations
            WriteWeaponInfo(); //table: weaponinfo
            WriteWeaponRefinements(); //table: weaponrefinements
            WriteArtifactSets(); //table: artifactsets
            WriteCharacterStats(); //table: characterstats
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
            temp = temp.Replace("42,\"Decreases the CD of Foul Legacy: Raging Tide by 20%.\",", "42,\"Palm Vortex pulls in opponents and objects within a 5m radius.\",");
            File.WriteAllText(path, temp);
        }

        void WriteWeaponInfo()
        {
            string path = csvFolderPath + "\\weaponinfo.csv";
            string temp = "id,name,weapon_type,rarity\n";
            foreach(Weapon w in weaponData)
            {
                try
                {
                    temp = temp + w.id + "," + w.name + "," + w.weapontype + "," + w.rarity +"\n";
                }
                catch(Exception)
                {
                    Console.WriteLine(w.name + ": error writing csv winfo file");
                    continue;
                }
                File.WriteAllText(path, temp);
            }
        }

        void WriteWeaponRefinements()
        {
            string path = csvFolderPath + "\\weaponrefinements.csv";
            string temp = "weaponid,r1,r2,r3,r4,r5\n";
            foreach(Weapon w in weaponData)
            {
                try
                {
                    temp = temp + w.id;
                    foreach(string r in w.refinements)
                    {
                        if (r == "-") temp = temp + ","; //set to null if no descriptions.
                        else temp = temp + ",\"" + r + "\"";
                    }
                    temp = temp + "\n";
                }
                catch(Exception)
                {
                    Console.WriteLine(w.name + ": Error writing csv wrefinements file");
                    continue;
                }
            }
            File.WriteAllText(path, temp);
        }

        void WriteArtifactSets()
        {
            string path = csvFolderPath + "\\artifactsets.csv";
            string temp = "id,name,twopiece,fourpiece,onepiece\n";
            foreach(Artifact a in artifactData)
            {
                temp = temp + a.id + "," + a.name + ",";
                if (a.twopiece is not null) temp = temp + $"\"{a.twopiece}\"";
                    temp = temp + ",";
                if (a.fourpiece is not null) temp = temp + $"\"{a.fourpiece}\"";
                    temp = temp + ",";
                if (a.onepiece is not null) temp = temp + $"\"{a.onepiece}\"";
                //check for null to prevent putting (,"", instead of ,,)
                temp = temp + "\n";
            }
            File.WriteAllText(path, temp);
        }

        void WriteCharacterStats()
        {
            string path = csvFolderPath + "\\characterstats.csv";
            string temp = "charid,levelid,hp,atk,def,ascensionstatname,ascensionstatvalue\n";
            foreach(CharacterStat cs in characterStatData)
            {
                temp = temp + cs.charid + "," + cs.levelid + "," + cs.hp + "," + cs.atk + "," + cs.def + "," + cs.ascensionname + "," + cs.ascensionvalue + "\n";
            }
            File.WriteAllText(path, temp);
        }
    }
}
