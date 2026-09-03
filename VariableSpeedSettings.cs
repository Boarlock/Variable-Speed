using Verse;

namespace VariableSpeed
{
    public class VariableSpeedSettings : ModSettings
    {
        // Enable or disable verbose logging for debugging purposes.
        public bool verboseLogging = false;
        public bool subOverrideDuration = true;

        public PlayerOverrideDuration playerOverrideDuration = PlayerOverrideDuration.TwelveHours;

        // Provide a static property to access the verbose logging setting from other classes.
        public static bool Verbose => VariableSpeedMod.settings != null && VariableSpeedMod.settings.verboseLogging;

        // Persist the verbose logging setting across game sessions.
        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref verboseLogging, "verboseLogging", false);

            Scribe_Values.Look(ref playerOverrideDuration, "playerOverrideDuration", PlayerOverrideDuration.TwelveHours);

            Scribe_Values.Look(ref subOverrideDuration, "subOverrideDuration", true);
        
        }
    }
    public enum PlayerOverrideDuration
    {
        SixHours = 6,
        TwelveHours = 12,
        TwentyFourHours = 24,
        FortyEightHours = 48
    }
}