using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace VariableSpeed
{
    public class SpeedConditions
    {
        public static bool CheckColonyIfAsleep()
        {

            List<Pawn> pawnList = Find.CurrentMap.mapPawns.FreeColonists;

            foreach (Pawn pawn in pawnList)
            {
                bool isSleepingJob = pawn.CurJob != null && pawn.CurJob.def == JobDefOf.LayDown;

                if (!isSleepingJob)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
