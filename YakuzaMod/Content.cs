using LethalLib.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Netcode.Components;
using UnityEngine;
using YakuzaMod.MonoBehaviours;

namespace YakuzaMod
{
    public class Content
    {
        public static AssetBundle MainAssets;
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
        public class CustomItem
        {
            public string name = "";
            public string itemPath = "";
            public string infoPath = "";
            public Action<Item> itemAction = (item) => { };
            public bool enabled = true;

            public CustomItem(string name, string itemPath, string infoPath, Action<Item> action = null)
            {
                this.name = name;
                this.itemPath = itemPath;
                this.infoPath = infoPath;
                if (action != null)
                {
                    itemAction = action;
                }
            }

            public static CustomItem Add(string name, string itemPath, string infoPath = null, Action<Item> action = null)
            {
                CustomItem item = new CustomItem(name, itemPath, infoPath, action);
                return item;
            }
        }

        public class CustomScrap : CustomItem
        {
            public Levels.LevelTypes levelType = Levels.LevelTypes.All;
            public int rarity = 0;

            public CustomScrap(string name, string itemPath, Levels.LevelTypes levelType, int rarity, Action<Item> action = null) : base(name, itemPath, null, action)
            {
                this.levelType = levelType;
                this.rarity = rarity;
            }

            public static CustomScrap Add(string name, string itemPath, Levels.LevelTypes levelType, int rarity, Action<Item> action = null)
            {
                CustomScrap item = new CustomScrap(name, itemPath, levelType, rarity, action);
                return item;
            }
        }

        public class CustomShopItem : CustomItem
        {
            public int itemPrice = 0;

            public CustomShopItem(string name, string itemPath, string infoPath = null, int itemPrice = 0, Action<Item> action = null) : base(name, itemPath, infoPath, action)
            {
                this.itemPrice = itemPrice;
            }

            public static CustomShopItem Add(string name, string itemPath, string infoPath = null, int itemPrice = 0, Action<Item> action = null, bool enabled = true)
            {
                CustomShopItem item = new CustomShopItem(name, itemPath, infoPath, itemPrice, action);
                item.enabled = enabled;
                return item;
            }
        }

        public static void TryLoadAssets()
        {
            if(MainAssets == null)
            {
                MainAssets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "yakuzamod"));
                Plugin.logger.LogInfo("Loaded asset bundle");
            }
        }

        public static List<CustomItem> customItems;
        public static void Load()
        {
            TryLoadAssets();

            customItems = new List<CustomItem>()
            {
                CustomScrap.Add("Kiryu", "Assets/Scrap/KazumaKiryu/Kiryu.asset", Levels.LevelTypes.All, Config.kiryuSpawnChance.Value),
                CustomScrap.Add("Majima", "Assets/Scrap/Majima/Majima.asset", Levels.LevelTypes.All, Config.majimaSpawnChance.Value),
                CustomScrap.Add("Yakuza0", "Assets/Scrap/Yakuza0Case/Yakuza0.asset", Levels.LevelTypes.All, Config.yakuza0SpawnChance.Value),
                CustomScrap.Add("YakuzaKiwami1", "Assets/Scrap/YakuzaK1Case/YakuzaKiwami1.asset", Levels.LevelTypes.All, Config.yakuzaK1SpawnChance.Value),
                CustomScrap.Add("YakuzaKiwami2", "Assets/Scrap/YakuzaK2Case/YakuzaKiwami2.asset", Levels.LevelTypes.All, Config.yakuzaK2SpawnChance.Value),
                CustomScrap.Add("YakuzaLikeADragon", "Assets/Scrap/YakuzaLADCase/YakuzaLikeADragon.asset", Levels.LevelTypes.All, Config.yakuzaLADSpawnChance.Value),
                CustomShopItem.Add("Staminan Royale", "Assets/ShopItem/StaminanRoyale/StaminanRoyale.asset", "Assets/ShopItem/StaminanRoyale/StaminanRoyaleInfo.asset", Config.staminanRoyalePrice.Value, enabled: true),
            };

            foreach(var item in customItems)
            {
                if(!item.enabled)
                {
                    continue;
                }

                var itemAsset = MainAssets.LoadAsset<Item>(item.itemPath);
                if(itemAsset.spawnPrefab.GetComponent<NetworkTransform>() == null && itemAsset.spawnPrefab.GetComponent<CustomNetworkTransform>() == null)
                {
                    var networkTransform = itemAsset.spawnPrefab.AddComponent<NetworkTransform>();
                    networkTransform.SlerpPosition = false;
                    networkTransform.Interpolate = false;
                    networkTransform.SyncPositionX = false;
                    networkTransform.SyncPositionY = false;
                    networkTransform.SyncPositionZ = false;
                    networkTransform.SyncScaleX = false;
                    networkTransform.SyncScaleY = false;
                    networkTransform.SyncScaleZ = false;
                    networkTransform.UseHalfFloatPrecision = true;
                }
                Prefabs.Add(item.name, itemAsset.spawnPrefab);
                NetworkPrefabs.RegisterNetworkPrefab(itemAsset.spawnPrefab);
                item.itemAction(itemAsset);
                
                if(item is CustomScrap)
                {
                    Plugin.logger.LogInfo($"Registering scrap item {item.name}");
                    Items.RegisterScrap(itemAsset, ((CustomScrap)item).rarity, ((CustomScrap)item).levelType);
                }
                else if(item is CustomShopItem)
                {
                    var itemInfo = MainAssets.LoadAsset<TerminalNode>(item.infoPath);
                    Plugin.logger.LogInfo($"Registering shop item {item.name} with price {((CustomShopItem)item).itemPrice}");
                    Items.RegisterShopItem(itemAsset, null, null, itemInfo, ((CustomShopItem)item).itemPrice);
                }

            }

            Plugin.logger.LogInfo("Loaded custom content!");
        }
    }
}
