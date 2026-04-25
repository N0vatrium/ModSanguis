using HarmonyLib;
using PB.Abilities;
using PB.Entities;
using PB.EntityProgression;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(AbilityScriptableObject), MethodType.Constructor)]
    internal static class Patch_Randomizer
    {
        public static List<AbilityScriptableObject> Abilities = [];
        public static Dictionary<ArchetypeScriptableObject, List<AbilityScriptableObject>> RandomizedAbilities = [];


        static void Postfix(AbilityScriptableObject __instance)
        {
            if (!PluginConfig.ConfigRandomizer.Value)
            {
                return;
            }

            if (!Abilities.Contains(__instance))
            {
                Abilities.Add(__instance);
                Helpers.WriteLog("Storing ability " + __instance.SkillName);
            }
        }
    }

    [HarmonyPatch(typeof(Entity), "Init", [typeof(EntityProgressionData), typeof(List<AbilityUpgradableData>)])]

    internal static class Patch_Randomizer_Entity_Init
    {
        public static ArchetypeScriptableObject BossArchetype = null;

        public static List<string> BlacklisEntity = ["bomb", "cocoon", "cinder"];
        public static List<string> BlacklistSpells = ["_execute_skill", "spawn", "cocoon", "cinder", "modifier_bleed_skill", "ability_movement", "modifier_"];

        static void Prefix(EntityProgressionData progressionData, ref List<AbilityUpgradableData> abilities, Entity __instance)
        {
            if (!PluginConfig.ConfigRandomizer.Value || abilities == null || abilities.Count == 0)
            {
                return;
            }
            var archetype = __instance.Archetype;

            if (BlacklisEntity.Any(banned => progressionData.Name.Contains(banned)))
            {
                return;
            }

            List<AbilityScriptableObject> newAbilities = [];


            while (newAbilities.Count < abilities.Count)
            {
                var random = Patch_Randomizer.Abilities[Random.Range(0, Patch_Randomizer.Abilities.Count - 1)];

                var blacklisted = BlacklistSpells.Any(banned => random.name.Contains(banned));

                if (!blacklisted)
                {
                    newAbilities.Add(random);
                }
            }

            abilities.Clear();

            foreach (var newAbility in newAbilities)
            {
                progressionData.AddAbility(newAbility);
            }

        }
    }
}
