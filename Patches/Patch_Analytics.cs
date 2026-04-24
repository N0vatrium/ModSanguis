using HarmonyLib;
using PB;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(AnalyticsManager), "m_ShouldSendEvents", MethodType.Getter)]
    [HarmonyPriority(Priority.First - 1)]
    internal static class Patch_Analytics
    {
        static bool Prefix(ref bool __result)
        {
            __result = false;
            
            // skip the original
            return false;
        }
    }
}
