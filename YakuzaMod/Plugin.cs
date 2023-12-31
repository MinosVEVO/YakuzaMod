using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;

namespace YakuzaMod
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "minos.Yakuza";
        public const string ModName = "Yakuza";
        public const string ModVersion = "0.0.1";

        public static ManualLogSource logger;
        public static ConfigFile config;
        private void Awake()
        {
            logger = Logger;
            config = Config;

            YakuzaMod.Config.Load();
            Content.Load();
            logger.LogInfo("Successfully loaded mod!");
        }
    }
}