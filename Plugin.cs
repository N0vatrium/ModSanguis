using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PB;
using System.Reflection;

namespace ModSanguis
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        public static PersistentData GameData;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            PluginConfig.Init(Config);

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Helpers.WriteLog($"{MyPluginInfo.PLUGIN_GUID} loaded! Let's stay chill with Argon");
        }
    }
}
