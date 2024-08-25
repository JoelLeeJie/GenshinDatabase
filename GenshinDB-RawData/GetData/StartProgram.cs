/*
Starting point of application.
This application scrapes data from Genshin Wiki web pages, and stores them into csv files on the pc.
Afterwards, each file is then read and inputted into the database.

Should be runned if database is to be updated.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinDB
{
    internal class Start
    {
        internal string thisFilePath;
        internal Start()
        {
            thisFilePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        }
        static void Main()
        {
            Start start = new Start();
            
            
/*            GetData getData = new GetData();
            //Gets Data from url. Runs get data on all 3 dataTypes at the same time.
            //Iterates through each name in the list, combines to get the corresponding url page, and stores the webpage's data into the corresponding dictionary
            Task.WaitAll(getData.GetDataForItemInList(getData.characterNames, dataType.characters).ToArray());
            Task.WaitAll(getData.GetDataForItemInList(getData.weaponNames, dataType.weapons).ToArray());
            Task.WaitAll(getData.GetDataForItemInList(getData.artifactNames, dataType.artifacts).ToArray());
            //WaitAll used to prevent all 3 methods running at same time, which will mess up value of "url"


            //stores data from GetData into several raw data files.
            WriteRawData writeData = new WriteRawData(start.thisFilePath);
            Task.WaitAll(writeData.WriteRawDataFromDictionary(getData.characterDict, dataType.characters).ToArray());
            Thread.Sleep(100);
            Task.WaitAll(writeData.WriteRawDataFromDictionary(getData.weaponDict, dataType.weapons).ToArray());
            Task.WaitAll(writeData.WriteRawDataFromDictionary(getData.artifactDict, dataType.artifacts).ToArray());
            //Do one by one, since doing all 3 at once will mess up value of "filePath"

            WikiData wiki = new WikiData(start.thisFilePath);
            wiki.GetCharacterData();
            wiki.GetWeaponData();
            Console.WriteLine("Finished loading data!");*/
            
            
            //convert raw data to struct to csv.
            //Extracts out useful information into structs and leaves behind excess.
            ConvertRawData convertRawData = new ConvertRawData(start.thisFilePath);
            //converts raw data to 3 different structs.
            convertRawData.CharacterRawData();
            convertRawData.ArtifactRawData();
            convertRawData.WeaponRawData();
            convertRawData.CharacterStatRawData();
            convertRawData.WeaponStatRawData();
            
            //Pass all csv formatted data to the class.
            WriteCSV writeCSV = new WriteCSV(convertRawData.characterData, convertRawData.artifactData, convertRawData.weaponData, convertRawData.characterStatData, convertRawData.weaponStatData,  start.thisFilePath);
            //Stores csv formatted data for all tables as csv files on the pc.
            writeCSV.WriteAllCSVFiles();
            Console.WriteLine("Finished writing csv files!");
            

        }
    }
}