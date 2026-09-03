using HarmonyLib;
using Verse;

namespace VariableSpeed
{
    [HarmonyPatch(typeof(TickManager), nameof(TickManager.CurTimeSpeed), MethodType.Setter)]
    
    public static class CurTimeSpeed_Patch
    {
        public static void Prefix(out TimeSpeed __state)
        {
            __state = Find.TickManager.CurTimeSpeed;
        }

        public static void Postfix(TimeSpeed value, TimeSpeed __state)
        {
            // Our mod changed the speed, so ignore this setter call.
            if (VariableSpeedState.VariableSpeedSetTime)
                return;

            // Player or RimWorld changed the time speed.
            if (value == TimeSpeed.Paused)
            {
                if (VariableSpeedMod.settings?.verboseLogging == true)
                {
                    Log.Message($"[VariableSpeed] Player has pause, Variable Speed is now paused.");
                }

                VariableSpeedState.VariableSpeedPaused = true;
            }
            else
            {
                VariableSpeedState.VariableSpeedPaused = false;
            }

            // Player has overridden time speed.
            if (value == TimeSpeed.Normal || value == TimeSpeed.Fast)
            {
                
                if (__state == TimeSpeed.Paused)
                {
                    if (VariableSpeedMod.settings?.verboseLogging == true)
                    {
                        Log.Message($"[VariableSpeed] Player has increased speed from pause, small override duration is enabled.");
                    }

                    // Player has simply unpaused into normal/fast, shorter override.
                    VariableSpeedState.VariableSpeedOverride = true;

                    VariableSpeedState.VariableSpeedOverrideUntil = Find.TickManager.TicksGame + 2500; // 2,500 ticks = 1 in-game hour.
                }
                else // Player has deliberately slowed time down, full override.
                {
                    if (VariableSpeedMod.settings?.verboseLogging == true)
                    {
                        Log.Message($"[VariableSpeed] Player has reduced speed, full override duration is enabled.");
                    }

                    VariableSpeedState.VariableSpeedOverride = true;

                    int hoursToTicks = (int)VariableSpeedMod.settings!.playerOverrideDuration * 2500;
                    VariableSpeedState.VariableSpeedOverrideUntil = Find.TickManager.TicksGame + hoursToTicks; // 30,000 ticks = 12 in-game hours is the default.
                }
            }
            
            if (value == TimeSpeed.Superfast)
            {
                if (VariableSpeedMod.settings?.subOverrideDuration == true)
                {
                    if (VariableSpeedMod.settings?.verboseLogging == true)
                    {
                        Log.Message($"[VariableSpeed] Player has changed speed to SuperFast, Variable Speed is unpaused.");
                    }

                    VariableSpeedState.VariableSpeedOverride = false;

                }
            }
        }
    }
}