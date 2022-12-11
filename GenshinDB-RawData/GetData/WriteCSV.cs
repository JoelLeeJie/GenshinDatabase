using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string temp = "id,name,weapontype,region,rarity,element\n";
            foreach(Character c in characterData)
            {
                try
                {
                    temp = temp + c.id + "," + c.name + "," + c.weapontype + "," + c.region + "," + c.rarity + "," + c.element + "\n";
                }
                catch (Exception)
                {
                    Console.WriteLine(c.name + ": write csv file");
                }
            }
            File.WriteAllText(path, temp);
        }
    }
}
