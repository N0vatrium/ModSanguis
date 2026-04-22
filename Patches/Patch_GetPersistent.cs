using HarmonyLib;
using PB;

namespace ModSanguis.Patches
{
    [HarmonyPatch(typeof(PersistentData), MethodType.Constructor)]
    [HarmonyPriority(Priority.First)]
    internal static class Patch_GetPersistent
    {
        static void Postfix(PersistentData __instance)
        {
            Plugin.GameData = __instance;

            //var recorder = Plugin.GameData.GameDataRecorder;
            //var combatStartedDelegate = new OnCombatStarted();

            //recorder.OnCombatStartedCallback = (Action<CombatContext.Data>)Delegate.Combine(recorder.OnCombatStartedCallback, new Action<CombatContext.Data>(combatStartedDelegate.GetStats));
        }
    }
}
