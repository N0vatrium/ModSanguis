using HarmonyLib;
using PB;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(MapContext), "ContextDataInput")]
    internal static class Patch_Debug
    {
        private static bool _enabled = false;
        static void Prefix(ContextData data, ref PersistentData persistentData, InternalGameSettingsSO internalGameSettingsSO)
        {
            if(!_enabled  || persistentData.CycleData.CycleProgressionData.GetAttribute(GameAttributes.CYCLE_MISSION_COUNT) < 2)
            {
                return;
            }

            var newCount = 1000;
            persistentData.CycleData.CycleProgressionData.SetAttribute(GameAttributes.TOTAL_BLOOD, newCount);

            //var size = persistentData.CycleData.CycleProgressionData.GetAttributeWithModifiers(GameAttributes.MAP_MOVEMENT_RANGE) + persistentData.CycleData.CycleProgressionData.GetAttributeWithModifiers(GameAttributes.MAP_NEXT_MOVEMENT_RANGE);
            //Helpers.WriteLog("Size is " + size);

            persistentData.CycleData.CycleProgressionData.SetAttribute(GameAttributes.MAP_MOVEMENT_RANGE, newCount * 6);
        }
    }
}
