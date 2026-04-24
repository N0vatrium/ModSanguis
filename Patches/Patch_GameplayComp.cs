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

                var percent = HealthPools[name];
                var maxHealth = __instance.GetAttributeWithModifiers(GameAttributes.MAX_HEALTH);


                var newHealth = maxHealth * percent;
                __instance.SetAttribute(GameAttributes.CURRENT_HEALTH, newHealth);

                Helpers.WriteLog("Loaded player " + name + " from the pools with " + newHealth);
            }
        }
    }
}
