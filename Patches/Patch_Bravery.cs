using HarmonyLib;
using PB;
using PB.EntityProgression;
using PB.EntityProgression.UI;
using System.Collections.Generic;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(TalentOfferScreen), "Init")]
    internal static class Patch_Bravery
    {
        public static TalentOfferScreenContext Context;

        private static bool _enabled = PluginConfig.ConfigBravery.Value;

        static void Postfix(TalentOfferScreen __instance)
        {
            if (!_enabled || Plugin.GameData.CycleData.IsTutorial)
            {
                return;
            }

            if (!Context)
            {
                Helpers.WriteLog("No context for bravery, aborting and disabling", error: true);
                _enabled = false;

                return;
            }

            Helpers.WriteLog("Player is picking talents");
            var traverse = Traverse.Create(__instance);
            var manager = traverse.Field("m_TalentOfferManager").GetValue<TalentOfferManager>();

            var managerTraverse = Traverse.Create(manager);
            var players = managerTraverse.Field("m_PlayableCharacters").GetValue<List<EntityProgressionData>>();
            var talents = managerTraverse.Field("m_CurrentTalentPicks").GetValue<Dictionary<EntityProgressionData, List<Talent>>>();

            for (int i = 0; i < players.Count; i++)
            {
                var entity = players[i];
                var playerTalents = talents[entity];
                var talent = playerTalents[UnityEngine.Random.Range(0, talents.Count - 1)];

                manager.SelectTalentForEntity(talent, entity);
                Helpers.WriteLog("Forced talent " + talent.Name + " for " + entity.Name);
            }

            traverse.Method("UpdateTalentButtonsState").GetValue();
            traverse.Method("UpdateValidationButtonState").GetValue();
            manager.ApplySelectedTalents();

            var contextTraverse = Traverse.Create(Context);

            // the event is registered one line after this hook in the parent context, I need enable then trigger it myself
            contextTraverse.Field("m_EnableInput").SetValue(true);
            contextTraverse.Method("TalentOfferScreen_OnTalentSelectionDone").GetValue();

        }
    }

    [HarmonyPatch(typeof(TalentOfferScreenContext), MethodType.Constructor)]
    internal static class Patch_Bravery_Context
    {
        static void Postfix(TalentOfferScreenContext __instance)
        {
            // Poor man's singleton lmao
            Patch_Bravery.Context = __instance;
        }
    }
}
