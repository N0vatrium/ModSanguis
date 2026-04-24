using BepInEx.Configuration;

namespace ModSanguis;
public class PluginConfig
{
    public static ConfigEntry<float> ConfigHealingMultiplier;

    public static void Init(ConfigFile configFile)
    {
        ConfigHealingMultiplier = configFile.Bind("CombatTweaks", "HealingMultiplier", 0.5f, "Used to reduce out of combat healing. 0 = no healing, 0.5 = 50% of missing health, 1 = full health (vanilla)");
    }
}
