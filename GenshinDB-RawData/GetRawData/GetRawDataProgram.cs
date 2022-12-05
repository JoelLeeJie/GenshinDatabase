using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace GenshinDB
{
    enum dataType {characters, weapons, artifacts}
    

    //This is an ExpandibleRegion
    #region GettingDataFromURL
    
    class GetData //gets data from api.genshin.dev (free informational resource, details on reddit). Stores data in dictionaries.
    {
        internal List<string> characterNames = new List<string>() { "albedo", "aloy", "amber", "arataki-itto", "ayaka", "ayato", "barbara", "beidou", "bennett", "chongyun", "collei", "diluc", "diona", "eula", "fischl", "ganyu", "gorou", "hu-tao", "jean", "kaeya", "kazuha", "keqing", "klee", "kokomi", "kuki-shinobu", "lisa", "mona", "ningguang", "noelle", "qiqi", "raiden", "razor", "rosaria", "sara", "sayu", "shenhe", "shikanoin-heizou", "sucrose", "tartaglia", "thoma", "tighnari", "traveler-anemo", "traveler-dendro", "traveler-electro", "traveler-geo", "venti", "xiangling", "xiao", "xingqiu", "xinyan", "yae-miko", "yanfei", "yelan", "yoimiya", "yun-jin", "zhongli" };
        internal List<string> weaponNames = new List<string>() { "akuoumaru", "alley-hunter", "amber-catalyst", "amenoma-kageuchi", "amos-bow", "apprentice-s-notes", "aquila-favonia", "beginner-s-protector", "black-tassel", "blackcliff-amulet", "blackcliff-longsword", "blackcliff-pole", "blackcliff-slasher", "blackcliff-warbow", "bloodtainted-greatsword", "calamity-queller", "cinnabar -spindle", "compound-bow", "cool-steel", "crescent-pike", "dark-iron-sword", "deathmatch", "debate-club", "dodoco-tales", "dragon-s-bane", "dragonspine-spear", "dull-blade", "eberlasting-moonglow", "ebony-bow", "elegy-for-the-end", "emerald-orb", "engulfing-lightning", "everlasting-moonglow", "eye-of-perception", "favonius-codex", "favonius-greatsword", "favonius-lance", "favonius-sword", "favonius-warbow", "ferrous-shadow", "festering-desire", "fillet-blade", "freedom-sworn", "frostbearer", "hakushin-ring", "halberd", "hamayumi", "haran-geppaku-futsu", "harbinger-of-dawn", "hunter-s-bow", "iron-point", "iron-sting", "kagura's-verity", "katsuragikiri-nagamasa", "kitain-cross-spear", "lion-s-roar", "lithic-blade", "lithic-spear", "lost-prayer-to-the-sacred-winds", "luxurious-sea-lord", "magic-guide", "mappa-mare", "memory-of-dust", "messenger", "mistsplitter-reforged", "mitternachts-waltz", "mouun's-moon", "oathsworn-eye", "old-merc-s-pal", "otherworldly-story", "pocket-grimoire", "polar-star", "predator", "primordial-jade-cutter", "primordial-jade-winged-spear", "prototype-archaic", "prototype-crescent", "prototype-grudge", "prototype-malice", "prototype-rancour", "quartz", "rainslasher", "raven-bow", "recurve-bow", "redhorn-stonethresher", "royal-bow", "royal-greatsword", "royal-grimoire", "royal-longsword", "royal-spear", "rust", "sacrificial-bow", "sacrificial-fragments", "sacrificial-greatsword", "sacrificial-sword", "seasoned-hunter-s-bow", "serpent-spine", "sharpshooter-s-oath", "silver-sword", "skyrider-greatsword", "skyrider-sword", "skyward-atlas", "skyward-blade", "skyward-harp", "skyward-pride", "skyward-spine", "slingshot", "snow-tombed-starsilver", "solar-pearl", "song-of-broken-pines", "staff-of-homa", "summit-shaper", "sword-of-descension", "the-alley-flash", "the-bell", "the-black-sword", "the-catch", "the-flute", "the-stringless", "the-unforged", "the-viridescent-hunt", "the-widsith", "thrilling-tales-of-dragon-slayers", "thundering-pulse", "traveler-s-handy-sword", "twin-nephrite", "vortex-vanquisher", "waster-greatsword", "wavebreaker's-fin", "white-iron-greatsword", "white-tassel", "whiteblind", "windblume-ode", "wine-and-song", "wolf-s-gravestone" };
        internal List<string> artifactNames = new List<string>() { "adventurer", "archaic-petra", "berserker", "blizzard-strayer", "bloodstained-chivalry", "brave-heart", "crimson-witch-of-flames", "deepwood-memories", "defender-s-will", "emblem-of-severed-fate", "gambler", "gilded-dreams", "glacier-and-snowfield", "gladiator-s-finale", "heart-of-depth", "husk-of-opulent-dreams", "instructor", "lavawalker", "lucky-dog", "maiden-beloved", "martial-artist", "noblesse-oblige", "ocean-hued-clam", "pale-flame", "prayers-for-destiny", "prayers-for-illumination", "prayers-for-wisdom", "prayers-to-springtime", "prayers-to-the-firmament", "resolution-of-sojourner", "retracing-bolide", "scholar", "shimenawa-s-reminiscence", "tenacity-of-the-millelith", "the-exile", "thundering-fury", "thundersoother", "tiny-miracle", "traveling-doctor", "viridescent-venerer", "wanderer-s-troupe" };
        internal Dictionary<string, Task<String>> characterDict = new Dictionary<string, Task<String>>() { };
        internal Dictionary<string, Task<String>> weaponDict = new Dictionary<string, Task<String>>() { };
        internal Dictionary<string, Task<String>> artifactDict = new Dictionary<string, Task<String>>() { };

        HttpClient webClient = new HttpClient();
        
        
        internal Task GetDataForItemInList(List<String> list, dataType type) //iterates through all names in given list, and gets results from website using those names. Puts results in respective dictionary depending on given enum type.
        {
            string url = "api.genshin.dev/";
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
                    return Task.CompletedTask;
            }

            foreach (string name in list)
            {
                try
                {
                    if (dictionary.ContainsKey(name)) continue;
                    dictionary.Add(name, GetDataFromURL(url)); //adds Task<string> raw data from website into dictionary, with keys as names.
                }
                catch (Exception e) { Console.WriteLine("error in GetDataForItemInList or GetDataFromURL" + e.Message + "name"); }
            }
            return Task.CompletedTask; //function completed, but Task<string> in dictionary may not be.
        }

        async Task<string> GetDataFromURL(string url)//access url, gets string and returns it as Task<string>.
        {
            return await Task.Run(() => webClient.GetStringAsync(url));
        }
    }
    #endregion
    class RawDataToStructReader //reads data from raw data files, and converts to structs.
    {
        
    }

    class Start //starts the process. GetData -> reads data and store as structs in lists --> convert to csv to port to database.
    {
        static void Main()
        {
            //Gets Data from url.
            GetData getData = new GetData(); 
            Task.WaitAll(getData.GetDataForItemInList(getData.characterNames, dataType.characters));
            getData.GetDataForItemInList(getData.weaponNames, dataType.weapons);
            getData.GetDataForItemInList(getData.artifactNames, dataType.artifacts);
        }
    }
}
