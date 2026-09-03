using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;

namespace VariableSpeed
{
        public class SpeedController
        {

            public static void SpeedAdjustSleep()
            {

                Find.TickManager.CurTimeSpeed = TimeSpeed.Superfast;

                VariableSpeedState.VariableSpeedSetTime = true;

            }
        }
}