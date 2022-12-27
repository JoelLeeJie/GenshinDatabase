using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinDB;
using System.Text.RegularExpressions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace RawData
{
    class Test
    {
        static void Main()
        { 
            List<string> weaponwikiNames = new List<string>() { "Akuoumaru", "Alley_Hunter", "Amber_Catalyst", "Amenoma_Kageuchi", "Amos_Bow", "Apprentice's_Notes", "Aquila_Favonia", "Beginner's_Protector", "Black_Tassel", "Blackcliff_Amulet", "Blackcliff_Longsword", "Blackcliff_Pole", "Blackcliff_Slasher", "Blackcliff_Warbow", "Bloodtainted_Greatsword", "Calamity_Queller", "Cinnabar _Spindle", "Compound_Bow", "Cool_Steel", "Crescent_Pike", "Dark_Iron_Sword", "Deathmatch", "Debate_Club", "Dodoco_Tales", "Dragon's_Bane", "Dragonspine_Spear", "Dull_Blade", "Ebony_Bow", "Elegy_For_The_End", "Emerald_Orb", "Engulfing_Lightning", "Everlasting_Moonglow", "Eye_Of_Perception", "Favonius_Codex", "Favonius_Greatsword", "Favonius_Lance", "Favonius_Sword", "Favonius_Warbow", "Ferrous_Shadow", "Festering_Desire", "Fillet_Blade", "Freedom_Sworn", "Frostbearer", "Hakushin_Ring", "Halberd", "Hamayumi", "Haran_Geppaku_Futsu", "Harbinger_Of_Dawn", "Hunter's_Bow", "Iron_Point", "Iron_Sting", "Kagura's_Verity", "Katsuragikiri_Nagamasa", "Kitain_Cross_Spear", "Lion's_Roar", "Lithic_Blade", "Lithic_Spear", "Lost_Prayer_To_The_Sacred_Winds", "Luxurious_Sea_Lord", "Magic_Guide", "Mappa_Mare", "Memory_Of_Dust", "Messenger", "Mistsplitter_Reforged", "Mitternachts_Waltz", "Mouun's_Moon", "Oathsworn_Eye", "Old_Merc's_Pal", "Otherworldly_Story", "Pocket_Grimoire", "Polar_Star", "Predator", "Primordial_Jade_Cutter", "Primordial_Jade_Winged_Spear", "Prototype_Archaic", "Prototype_Crescent", "Prototype_Grudge", "Prototype_Malice", "Prototype_Rancour", "Quartz", "Rainslasher", "Raven_Bow", "Recurve_Bow", "Redhorn_Stonethresher", "Royal_Bow", "Royal_Greatsword", "Royal_Grimoire", "Royal_Longsword", "Royal_Spear", "Rust", "Sacrificial_Bow", "Sacrificial_Fragments", "Sacrificial_Greatsword", "Sacrificial_Sword", "Seasoned_Hunter's_Bow", "Serpent_Spine", "Sharpshooter's_Oath", "Silver_Sword", "Skyrider_Greatsword", "Skyrider_Sword", "Skyward_Atlas", "Skyward_Blade", "Skyward_Harp", "Skyward_Pride", "Skyward_Spine", "Slingshot", "Snow_Tombed_Starsilver", "Solar_Pearl", "Song_Of_Broken_Pines", "Staff_Of_Homa", "Summit_Shaper", "Sword_Of_Descension", "The_Alley_Flash", "The_Bell", "The_Black_Sword", "The_Catch", "The_Flute", "The_Stringless", "The_Unforged", "The_Viridescent_Hunt", "The_Widsith", "Thrilling_Tales_Of_Dragon_Slayers", "Thundering_Pulse", "Traveler's_Handy_Sword", "Twin_Nephrite", "Vortex_Vanquisher", "Waster_Greatsword", "Wavebreaker's_Fin", "White_Iron_Greatsword", "White_Tassel", "Whiteblind", "Windblume_Ode", "Wine_And_Song", "Wolf's_Gravestone" };

        }
    }
}


