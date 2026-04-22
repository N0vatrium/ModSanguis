using HarmonyLib;
using PB;
using PB.Combat;
using PB.Entities;

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

            Patch_GameplayComp.HealthPools.Clear();

            foreach (Entity allyUnit in repo.AllyUnits)
            {
                var playerId = allyUnit.Name;

                var health = allyUnit.SimulationData.GetAttribute(GameAttributes.CURRENT_HEALTH);
                if(health < 0)
                {
                    health = 0;
                }

                var maxHealth = allyUnit.SimulationData.GetAttribute(GameAttributes.MAX_HEALTH);

                if(health == maxHealth)
                {
                    Helpers.WriteLog("Skipping healty player " + playerId);

                    return;
                }

                Patch_GameplayComp.HealthPools.Add(playerId, health);
                Helpers.WriteLog("Stored " + health + " for " + playerId);
            }
        }
    }
}
