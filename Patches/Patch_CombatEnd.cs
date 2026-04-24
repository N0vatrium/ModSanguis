using HarmonyLib;
using PB;
using PB.Combat;
using PB.Entities;
using UnityEngine;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(CombatSequencer), "CombatEnd")]
    internal static class Patch_CombatEnd
    {
        static void Prefix(CombatSequencer __instance)
        {
            Helpers.WriteLog("Player ended combat");
            Patch_CombatEnd_Shared.ProcessSequence(__instance);
        }
    }

    [HarmonyPatch(typeof(CombatContext), "OnCombatLeft")]
    internal static class Patch_CombatEnd_Abandon
    {
        static void Prefix(CombatContext __instance)
        {
            Helpers.WriteLog("Player left combat");
            Patch_CombatEnd_Shared.ProcessSequence(__instance.CombatSequencer);
        }
    }

    [HarmonyPatch(typeof(GameDataRecorder), "OnNewCycle")]
    internal static class Patch_CombatEnd_NewCycle
    {
        static void Prefix()
        {
            Helpers.WriteLog("New cycle, clearing health pools, good luck!");
            Patch_GameplayComp.HealthPools.Clear();
        }
    }

    internal static class Patch_CombatEnd_Shared
    {
        public static void ProcessSequence(CombatSequencer combatSequencer)
        {
            var traverse = Traverse.Create(combatSequencer).Field("m_UnitRepository");

            var repo = traverse.GetValue<EntityRepository>();

            var healingMultiplier = PluginConfig.ConfigHealingMultiplier.Value;

            if(healingMultiplier < 0)
            {
                Helpers.WriteLog("You set the healing reduction too low! Increasing it to 0% to avoid errors", error: true);
                healingMultiplier = 0;
            }

            Patch_GameplayComp.HealthPools.Clear();

            foreach (Entity allyUnit in repo.AllyUnits)
            {
                var playerId = allyUnit.Name;

                var health = allyUnit.SimulationData.GetAttributeWithModifiers(GameAttributes.CURRENT_HEALTH);
                if (health < 0)
                {
                    health = 0;
                }

                var maxHealth = allyUnit.SimulationData.GetAttributeWithModifiers(GameAttributes.MAX_HEALTH);
                var percent = health / maxHealth;
                var missing = 1 - percent;

                health = Mathf.Clamp(percent + missing * healingMultiplier, 0f, 1f);

                if (health == 1f)
                {
                    Helpers.WriteLog("Skipping healty player: " + playerId);

                    continue;
                }



                Patch_GameplayComp.HealthPools.Add(playerId, health);
                Helpers.WriteLog("Stored " + health + " for " + playerId);
            }
        }
    }
}
