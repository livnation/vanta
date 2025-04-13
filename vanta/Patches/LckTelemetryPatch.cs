using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Liv.Lck.Telemetry;

namespace vanta.Patches
{
    [HarmonyPatch]
    internal static class LckTelemetryPatch
    {
        static MethodBase TargetMethod()
        {
            var nestedType = typeof(LckTelemetry).GetNestedType("MetaData", BindingFlags.NonPublic);
            return AccessTools.Method(nestedType, "Collect");
        }

        static bool Prefix(string runId, object geoLocation, string userId)
        {   
            return false;
        }
    }
}
