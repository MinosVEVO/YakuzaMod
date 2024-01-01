using JetBrains.Annotations;
using System;
using BepInEx;
using BepInEx.Configuration;
using System.Collections.Generic;
using System.Diagnostics;

namespace YakuzaMod
{
    public class Config
    {
        public static ConfigEntry<int> kiryuSpawnChance;
        public static ConfigEntry<int> yakuza0SpawnChance;
        public static ConfigEntry<int> yakuzaK1SpawnChance;
        public static ConfigEntry<int> yakuzaK2SpawnChance;
        public static ConfigEntry<int> yakuzaLADSpawnChance;
        public static ConfigEntry<int> staminanRoyalePrice;
        public static ConfigEntry<int> majimaSpawnChance;

        public static void Load()
        {
            kiryuSpawnChance = Plugin.config.Bind<int>("Scrap", "Kiryu-Chan! :3", 3, "How much Kiryu spawns, higher = more");
            majimaSpawnChance = Plugin.config.Bind<int>("Scrap", "Majima :3", 3, "How much Majima spawns, higher = more");
            yakuza0SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza 0", 20, "How much the Yakuza 0 Game spawns, higher = more");
            yakuzaK1SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Kiwami 1", 20, "How much the Yakuza Kiwami 1 Game spawns, higher = more");
            yakuzaK2SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Kiwami 2", 20, "How much the Yakuza Kiwami 1 Game spawns, higher = more");
            yakuzaLADSpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Like A Dragon", 20, "How much the Like A Dragon Game spawns, higher = more");
            staminanRoyalePrice = Plugin.config.Bind<int>("Store", "Staminan Royale", 500, "How much the healing item costs");
        }
    }
}


