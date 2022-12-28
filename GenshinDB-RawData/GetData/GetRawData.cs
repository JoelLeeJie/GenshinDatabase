using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GenshinDB
{
    enum dataType { characters, weapons, artifacts }


    //This is an ExpandibleRegion
    #region GettingDataFromURL

    class GetData //gets data from api.genshin.dev (free informational resource, details on reddit). Stores data in dictionaries.
    {
        internal List<string> characterNames = new List<string>() { "albedo", "aloy", "amber", "arataki-itto", "ayaka", "ayato", "barbara", "beidou", "bennett", "chongyun", "collei", "diluc", "diona", "eula", "fischl", "ganyu", "gorou", "hu-tao", "jean", "kaeya", "kazuha", "keqing", "klee", "kokomi", "kuki-shinobu", "lisa", "mona", "ningguang", "noelle", "qiqi", "raiden", "razor", "rosaria", "sara", "sayu", "shenhe", "shikanoin-heizou", "sucrose", "tartaglia", "thoma", "tighnari", "traveler-anemo", "traveler-dendro", "traveler-electro", "traveler-geo", "venti", "xiangling", "xiao", "xingqiu", "xinyan", "yae-miko", "yanfei", "yelan", "yoimiya", "yun-jin", "zhongli" };
        internal List<string> weaponNames = new List<string>() { "akuoumaru", "alley-hunter", "amber-catalyst", "amenoma-kageuchi", "amos-bow", "apprentice-s-notes", "aquila-favonia", "beginner-s-protector", "black-tassel", "blackcliff-amulet", "blackcliff-longsword", "blackcliff-pole", "blackcliff-slasher", "blackcliff-warbow", "bloodtainted-greatsword", "calamity-queller", "cinnabar -spindle", "compound-bow", "cool-steel", "crescent-pike", "dark-iron-sword", "deathmatch", "debate-club", "dodoco-tales", "dragon-s-bane", "dragonspine-spear", "dull-blade", "ebony-bow", "elegy-for-the-end", "emerald-orb", "engulfing-lightning", "everlasting-moonglow", "eye-of-perception", "favonius-codex", "favonius-greatsword", "favonius-lance", "favonius-sword", "favonius-warbow", "ferrous-shadow", "festering-desire", "fillet-blade", "freedom-sworn", "frostbearer", "hakushin-ring", "halberd", "hamayumi", "haran-geppaku-futsu", "harbinger-of-dawn", "hunter-s-bow", "iron-point", "iron-sting", "kagura's-verity", "katsuragikiri-nagamasa", "kitain-cross-spear", "lion-s-roar", "lithic-blade", "lithic-spear", "lost-prayer-to-the-sacred-winds", "luxurious-sea-lord", "magic-guide", "mappa-mare", "memory-of-dust", "messenger", "mistsplitter-reforged", "mitternachts-waltz", "mouun's-moon", "oathsworn-eye", "old-merc-s-pal", "otherworldly-story", "pocket-grimoire", "polar-star", "predator", "primordial-jade-cutter", "primordial-jade-winged-spear", "prototype-archaic", "prototype-crescent", "prototype-grudge", "prototype-malice", "prototype-rancour", "quartz", "rainslasher", "raven-bow", "recurve-bow", "redhorn-stonethresher", "royal-bow", "royal-greatsword", "royal-grimoire", "royal-longsword", "royal-spear", "rust", "sacrificial-bow", "sacrificial-fragments", "sacrificial-greatsword", "sacrificial-sword", "seasoned-hunter-s-bow", "serpent-spine", "sharpshooter-s-oath", "silver-sword", "skyrider-greatsword", "skyrider-sword", "skyward-atlas", "skyward-blade", "skyward-harp", "skyward-pride", "skyward-spine", "slingshot", "snow-tombed-starsilver", "solar-pearl", "song-of-broken-pines", "staff-of-homa", "summit-shaper", "sword-of-descension", "the-alley-flash", "the-bell", "the-black-sword", "the-catch", "the-flute", "the-stringless", "the-unforged", "the-viridescent-hunt", "the-widsith", "thrilling-tales-of-dragon-slayers", "thundering-pulse", "traveler-s-handy-sword", "twin-nephrite", "vortex-vanquisher", "waster-greatsword", "wavebreaker's-fin", "white-iron-greatsword", "white-tassel", "whiteblind", "windblume-ode", "wine-and-song", "wolf-s-gravestone" };
        internal List<string> artifactNames = new List<string>() { "adventurer", "archaic-petra", "berserker", "blizzard-strayer", "bloodstained-chivalry", "brave-heart", "crimson-witch-of-flames", "deepwood-memories", "defender-s-will", "emblem-of-severed-fate", "gambler", "gilded-dreams", "glacier-and-snowfield", "gladiator-s-finale", "heart-of-depth", "husk-of-opulent-dreams", "instructor", "lavawalker", "lucky-dog", "maiden-beloved", "martial-artist", "noblesse-oblige", "ocean-hued-clam", "pale-flame", "prayers-for-destiny", "prayers-for-illumination", "prayers-for-wisdom", "prayers-to-springtime", "prayers-to-the-firmament", "resolution-of-sojourner", "retracing-bolide", "scholar", "shimenawa-s-reminiscence", "tenacity-of-the-millelith", "the-exile", "thundering-fury", "thundersoother", "tiny-miracle", "traveling-doctor", "viridescent-venerer", "wanderer-s-troupe" };
        internal Dictionary<string, Task<String>> characterDict = new Dictionary<string, Task<String>>() { };
        internal Dictionary<string, Task<String>> weaponDict = new Dictionary<string, Task<String>>() { };
        internal Dictionary<string, Task<String>> artifactDict = new Dictionary<string, Task<String>>() { };


        HttpClient webClient = new HttpClient();

        internal List<Task> GetDataForItemInList(List<String> list, dataType type) //iterates through all names in given list, and gets results from website using those names. Puts results in respective dictionary depending on given enum type.
        {
            List<Task> taskList = new List<Task>();
            string url = "https://api.genshin.dev/";
            Dictionary<string, Task<string>> dictionary = characterDict; //characterDict is a placeholder, as compiler throws unassigned error otherwise.
            switch (type)
            {
                case dataType.characters:
                    url = url + "characters";
                    dictionary = characterDict; //sets dictionary to be of "characters". Works as dictionary is reference type.
                    break;
                case dataType.weapons:
                    url = url + "weapons";
                    dictionary = weaponDict;
                    break;
                case dataType.artifacts:
                    url = url + "artifacts";
                    dictionary = artifactDict;
                    break;
                default:
                    Console.WriteLine("Wrong enum type for GetDataForItemInList");
                    return new List<Task>();
            }
            foreach (string name in list)
            {
                try
                {
                    if (dictionary.ContainsKey(name)) continue;
                    dictionary.Add(name, Task.Run(() => GetDataFromURL(url + "/" + name))); //adds Task<string> raw data from website into dictionary, with keys as names.
                    taskList.Add(dictionary[name]);
                }
                catch (Exception e) { Console.WriteLine("error in GetDataForItemInList or GetDataFromURL" + e.Message + "name"); }
            }
            return taskList;
        }

        Task<string> GetDataFromURL(string url)//access url, gets string and returns it as Task<string>.
        {
            return webClient.GetStringAsync(url);
        }
    }
    #endregion

    class WriteRawData //writes raw data into raw data files.
    {
        string rawDataFilePath;

        internal WriteRawData(string filePath)
        {
            rawDataFilePath = Directory.GetParent(filePath).FullName + "\\RawDataFiles(txt)"; //finds file path of raw data folder
        }
        internal List<Task> WriteRawDataFromDictionary(Dictionary<string, Task<String>> dictionary, dataType type) //writes data into several files from a dictionary, where name of file is the key, its contents being the value of that key.
        {
            List<Task> taskList = new List<Task>();
            string tempPath = rawDataFilePath;
            switch (type) //sorts raw data into different folders. Do note not to do all 3 types at once to avoid messing up this.
            {
                case dataType.characters:
                    tempPath = tempPath + "\\Characters";
                    break;
                case dataType.weapons:
                    tempPath = tempPath + "\\Weapons";
                    break;
                case dataType.artifacts:
                    tempPath = tempPath + "\\Artifacts";
                    break;
                default:
                    Console.WriteLine("Wrong enum type at WriteRawDataFromDictionary");
                    return new List<Task>() { };
            }
            foreach (string name in dictionary.Keys) //foreach name in the dictionary, create a raw data file containing the value for that name.
            {
                taskList.Add(Task.Run(() => File.WriteAllText(tempPath + "\\" + name + ".txt", dictionary[name].Result)));
            }
            Console.WriteLine(tempPath);
            return taskList;
        }
    }

    class WikiData
    {
        
        List<string> characterwikiNames = new List<string>() { "Albedo", "Aloy", "Amber", "Arataki_Itto", "Ayaka", "Ayato", "Barbara", "Beidou", "Bennett", "Chongyun", "Collei", "Diluc", "Diona", "Eula", "Fischl", "Ganyu", "Gorou", "Hu_Tao", "Jean", "Kaeya", "Kazuha", "Keqing", "Klee", "Kokomi", "Kuki_Shinobu", "Lisa", "Mona", "Ningguang", "Noelle", "Qiqi", "Raiden_Shogun", "Razor", "Rosaria", "Kujou_Sara", "Sayu", "Shenhe", "Shikanoin_Heizou", "Sucrose", "Tartaglia", "Thoma", "Tighnari", "Traveler_(Anemo)", "Traveler_(Dendro)", "Traveler_(Electro)", "Traveler_(Geo)", "Venti", "Xiangling", "Xiao", "Xingqiu", "Xinyan", "Yae_Miko", "Yanfei", "Yelan", "Yoimiya", "Yun_Jin", "Zhongli" };
        List<string> weaponwikiNames = new List<string>() { "Akuoumaru", "Alley_Hunter", "Amber_Catalyst", "Amenoma_Kageuchi", "Amos_Bow", "Apprentice's_Notes", "Aquila_Favonia", "Beginner's_Protector", "Black_Tassel", "Blackcliff_Amulet", "Blackcliff_Longsword", "Blackcliff_Pole", "Blackcliff_Slasher", "Blackcliff_Warbow", "Bloodtainted_Greatsword", "Calamity_Queller", "Cinnabar _Spindle", "Compound_Bow", "Cool_Steel", "Crescent_Pike", "Dark_Iron_Sword", "Deathmatch", "Debate_Club", "Dodoco_Tales", "Dragon's_Bane", "Dragonspine_Spear", "Dull_Blade", "Ebony_Bow", "Elegy_for_the_End", "Emerald_Orb", "Engulfing_Lightning", "Everlasting_Moonglow", "Eye_of_Perception", "Favonius_Codex", "Favonius_Greatsword", "Favonius_Lance", "Favonius_Sword", "Favonius_Warbow", "Ferrous_Shadow", "Festering_Desire", "Fillet_Blade", "Freedom-Sworn", "Frostbearer", "Hakushin_Ring", "Halberd", "Hamayumi", "Haran_Geppaku_Futsu", "Harbinger_of_Dawn", "Hunter's_Bow", "Iron_Point", "Iron_Sting", "Kagura's_Verity", "Katsuragikiri_Nagamasa", "Kitain_Cross_Spear", "Lion's_Roar", "Lithic_Blade", "Lithic_Spear", "Lost_Prayer_to_the_Sacred_Winds", "Luxurious_Sea-Lord", "Magic_Guide", "Mappa_Mare", "Memory_of_Dust", "Messenger", "Mistsplitter_Reforged", "Mitternachts_Waltz", "Mouun's_Moon", "Oathsworn_Eye", "Old_Merc's_Pal", "Otherworldly_Story", "Pocket_Grimoire", "Polar_Star", "Predator", "Primordial_Jade_Cutter", "Primordial_Jade_Winged-Spear", "Prototype_Archaic", "Prototype_Crescent", "Prototype_Grudge", "Prototype_Malice", "Prototype_Rancour", "Quartz", "Rainslasher", "Raven_Bow", "Recurve_Bow", "Redhorn_Stonethresher", "Royal_Bow", "Royal_Greatsword", "Royal_Grimoire", "Royal_Longsword", "Royal_Spear", "Rust", "Sacrificial_Bow", "Sacrificial_Fragments", "Sacrificial_Greatsword", "Sacrificial_Sword", "Seasoned_Hunter's_Bow", "Serpent_Spine", "Sharpshooter's_Oath", "Silver_Sword", "Skyrider_Greatsword", "Skyrider_Sword", "Skyward_Atlas", "Skyward_Blade", "Skyward_Harp", "Skyward_Pride", "Skyward_Spine", "Slingshot", "Snow-Tombed_Starsilver", "Solar_Pearl", "Song_of_Broken_Pines", "Staff_of_Homa", "Summit_Shaper", "Sword_of_Descension", "The_Alley_Flash", "The_Bell", "The_Black_Sword", "The_Catch", "The_Flute", "The_Stringless", "The_Unforged", "The_Viridescent_Hunt", "The_Widsith", "Thrilling_Tales_of_Dragon_Slayers", "Thundering_Pulse", "Traveler's_Handy_Sword", "Twin_Nephrite", "Vortex_Vanquisher", "Waster_Greatsword", "Wavebreaker's_Fin", "White_Iron_Greatsword", "White_Tassel", "Whiteblind", "Windblume_Ode", "Wine_and_Song", "Wolf's_Gravestone" };

        HttpClient client1 = new HttpClient();
        HttpClient client2 = new HttpClient();
        string rawDataFilePath;
        string temp;
        internal WikiData(string path)
        {
            rawDataFilePath = Directory.GetParent(path).FullName + "\\RawDataFiles(txt)";
        }

        internal void GetCharacterData()
        {
            string path = rawDataFilePath + "\\CharacterWiki\\";
            foreach(string name in characterwikiNames)
            {
                File.WriteAllText(path + name + "wiki.txt", client1.GetStringAsync("https://genshin-impact.fandom.com/wiki/" + name).Result);
            }
        }

        internal void GetWeaponData()
        {
            string path = rawDataFilePath + "\\WeaponWiki\\";
            foreach(string name in weaponwikiNames)
            {
                File.WriteAllText(path + name + "wiki.txt", client2.GetStringAsync("https://genshin-impact.fandom.com/wiki/" + name).Result);
            }
        }
    }


    
}
