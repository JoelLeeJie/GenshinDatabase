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
        }

        void WriteCharacterInfo()
        {
            string path = csvFolderPath + "\\characterinfo.csv";
            string temp = "id,name,description,element,weapon_type,region,rarity\n";
            foreach(Character c in characterData)
            {
                try
                {
                    temp = temp + c.id + "," + c.name + "," +"\"" + c.description + "\"" + "," + c.element + "," + c.weapontype + "," + c.region + "," + c.rarity + "\n";
                    temp = temp.Replace("\\\"", "'"); //replaces /" with '
                } //"" around description to ignore extra commas when copying.
                catch (Exception)
                {
                    Console.WriteLine(c.name + ": write csv file");
                }
            }
            File.WriteAllText(path, temp);

        }
    }
}
