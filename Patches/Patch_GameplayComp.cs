using HarmonyLib;
using PB;
using PB.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(EntityGameplayAbilityComponent), MethodType.Constructor, [typeof(Entity), typeof(GameObject)])]
    internal static class Patch_GameplayComp
    {
        public static Dictionary<string, float> HealthPools = [];

        static void Postfix(EntityGameplayAbilityComponent __instance)
        {
            var name = __instance.Name;
            if (HealthPools.ContainsKey(name))
            {
                Helpers.WriteLog("Loading player "+ __instance.Name + " from the pools with " + HealthPools[name]);

                var health = HealthPools[name];
                var maxHealth = __instance.AttributeSet.GetValue(GameAttributes.MAX_HEALTH);

                // half of missing health
                var half = (maxHealth - health) /2 ;
                Helpers.WriteLog("Half is " + half);

                __instance.AttributeSet.Set(GameAttributes.CURRENT_HEALTH, health + half);
            }
        }
    }
}
