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
        public static ConfigEntry<int> cupcakeSpawnChance;
        public static ConfigEntry<int> kiryuSpawnChance;
        public static ConfigEntry<int> yakuza0SpawnChance;
        public static ConfigEntry<int> yakuzaK1SpawnChance;
        public static ConfigEntry<int> yakuzaK2SpawnChance;
        public static ConfigEntry<int> yakuzaLADSpawnChance;
        public static ConfigEntry<int> staminanRoyalePrice;

        public static void Load()
        {
            cupcakeSpawnChance = Plugin.config.Bind<int>("Scrap", "Cupcake", 100, "How much does Cupcake spawn, higher = more");
            kiryuSpawnChance = Plugin.config.Bind<int>("Scrap", "Kiryu-Chan! :3", 100, "How much Kiryu spawns, higher = more");
            yakuza0SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza 0", 100, "How much the Yakuza 0 Game spawns, higher = more");
            yakuzaK1SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Kiwami 1", 100, "How much the Yakuza Kiwami 1 Game spawns, higher = more");
            yakuzaK2SpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Kiwami 2", 100, "How much the Yakuza Kiwami 1 Game spawns, higher = more");
            yakuzaLADSpawnChance = Plugin.config.Bind<int>("Scrap", "Yakuza Like A Dragon", 100, "How much the Like A Dragon Game spawns, higher = more");
            staminanRoyalePrice = Plugin.config.Bind<int>("Store", "Staminan Royale", 1000, "How much the healing item costs");
        }
    }
}


