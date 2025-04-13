using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using Liv.Lck.Telemetry;

namespace vanta.patches
{
    [HarmonyPatch]
    internal static class LckTelemetryPatch
    {
        static MethodBase TargetMethod()
        {
            var nt = typeof(LckTelemetry).GetNestedType("MetaData", BindingFlags.NonPublic);
            return AccessTools.Method(nt, "Collect");
        }

        static bool Prefix(string runId, object geoLocation, string userId) => false;
    }
}
