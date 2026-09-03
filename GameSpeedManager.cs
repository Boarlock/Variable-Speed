using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace VariableSpeed
{
    public class GameSpeedManager : GameComponent
    {
        private int asleepTicks = 0;
        public GameSpeedManager(Game game) { }
        public override void GameComponentTick()
        {

            asleepTicks++;

            if (asleepTicks % 60 != 0)
                return;

            // Reset this flag.
            VariableSpeedState.VariableSpeedSetTime = false;

            // If the override is disabled and time has exceeded the override duration.
            if (VariableSpeedState.VariableSpeedOverride && Find.TickManager.TicksGame >= VariableSpeedState.VariableSpeedOverrideUntil)
            {
                if (VariableSpeedMod.settings?.verboseLogging == true)
                {
                    Log.Message($"[VariableSpeed] Player override duration has lapsed, Variable Speed is unpaused.");
                }

                VariableSpeedState.VariableSpeedOverride = false;
            }

            // Check if all colonists are asleep, and the current time speed is not Superfast, and variable speed is not paused or overridden.
            if (!(Find.TickManager.CurTimeSpeed == TimeSpeed.Superfast) && !VariableSpeedState.VariableSpeedPaused && !VariableSpeedState.VariableSpeedOverride && SpeedConditions.CheckColonyIfAsleep())
            {

                if (VariableSpeedMod.settings?.verboseLogging == true)
                {
                    Log.Message($"[VariableSpeed] All pawns are sleeping, speeding up game time.");
                }

                SpeedController.SpeedAdjustSleep();
            }
        }
    }

    public static class VariableSpeedState
    {
        public static bool VariableSpeedSetTime = false;
        public static bool VariableSpeedPaused = false;
        public static bool VariableSpeedOverride = false;
        public static int VariableSpeedOverrideUntil = 0;
    }
}
