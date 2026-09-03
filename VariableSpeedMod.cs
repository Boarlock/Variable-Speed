using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace VariableSpeed
{
    public class VariableSpeedMod : Mod
    {
        public VariableSpeedMod(ModContentPack content) : base(content)
        {
            // 1. Initialize Settings
            settings = GetSettings<VariableSpeedSettings>();

            // 2. Initialize Harmony
            var harmony = new Harmony("b0arl0ck.variablespeed");
            harmony.PatchAll();

            // 3. Log Initialization
            Log.Message($"[VariableSpeed] Initialization completed.");
        }
        public static VariableSpeedSettings? settings;

        public override string SettingsCategory() => "Variable Speed";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Gap(8f);

            listing.CheckboxLabeled("Enable Developer Logging", ref settings!.verboseLogging);

            listing.Gap(8f);

            listing.CheckboxLabeled("Unpause Variable Speed when game time is set to SuperFast", ref settings!.subOverrideDuration);

            listing.Gap(12f);

            string durationLabel = $"{(int)settings.playerOverrideDuration} In-Game hours";

            if (listing.ButtonTextLabeled("Player Override Duration", durationLabel))
            {
                List<FloatMenuOption> options = new List<FloatMenuOption>
                {
                    new FloatMenuOption("6 hours", () => settings.playerOverrideDuration = PlayerOverrideDuration.SixHours),

                    new FloatMenuOption("12 hours", () => settings.playerOverrideDuration = PlayerOverrideDuration.TwelveHours),

                    new FloatMenuOption("24 hours", () => settings.playerOverrideDuration = PlayerOverrideDuration.TwentyFourHours),

                    new FloatMenuOption("48 hours", () => settings.playerOverrideDuration = PlayerOverrideDuration.FortyEightHours)
                };

                Find.WindowStack.Add(new FloatMenu(options));
            }

            listing.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
