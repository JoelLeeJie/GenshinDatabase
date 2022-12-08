using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GenshinDB
{
    internal class ConvertRawData
    {
        string rawDataFilePath;
        string characterRawDataPath;
        string artifactRawDataPath;
        string weaponRawDataPath;
        internal ConvertRawData(string filePath)
        {
            rawDataFilePath = Directory.GetParent(filePath).FullName + "\\RawDataFiles(txt)";
            characterRawDataPath = rawDataFilePath + "\\Characters";
            artifactRawDataPath = rawDataFilePath + "\\Artifacts";
            weaponRawDataPath = rawDataFilePath + "\\Weapons";
        }
        internal void CharacterRawData()
        {
            

        }

        internal void ArtifactRawData()
        {

        }

        internal void WeaponRawData()
        {

        }
    }

    internal class WriteCSVFiles
    {

    }

    internal struct Character
    {

    }

    internal struct Artifact
    {

    }

    internal struct Weapon
    {

    }
}
