using PB.Combat;

namespace ModSanguis.Delegates
{
    internal class OnCombatStarted
    {
        public void GetStats(CombatContext.Data combatContextData)
        {
            Helpers.WriteLog($"Current mission is {combatContextData.PersistentData.CycleData.CurrentMissionIndex}");
        }
    }
}
