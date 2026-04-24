using BepInEx.Configuration;

namespace ModSanguis;
public class PluginConfig
{
    public static ConfigEntry<float> ConfigHealingMultiplier;
    public static ConfigEntry<bool> ConfigBravery;

    public static void Init(ConfigFile configFile)
    {
        ConfigBravery = configFile.Bind("GameplayTweaks", "Bravery", false, "When set to true, you won't be able to choose your talent and a random one will be picked instead");

        ConfigHealingMultiplier = configFile.Bind("CombatTweaks", "HealingMultiplier", 0.5f, "Used to reduce out of combat healing. 0 = no healing, 0.5 = 50% of missing health, 1 = full health (vanilla)");
    }
}
