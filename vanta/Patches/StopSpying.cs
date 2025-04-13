using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backtrace.Unity.Model;
using Backtrace.Unity.Types;
using HarmonyLib;

namespace vanta.patches
{
    internal static class StopSpying
    {
        // This isnt for anticheat reporting, I just dont like unity constantly sending
        // Peoples performance data to them, theres no reason for it.
        [HarmonyPatch(typeof(Backtrace.Unity.BacktraceClient), "SendReport")]
        internal static class ReportData
        {
            static bool Prefix(BacktraceReport report, Action<BacktraceResult> sendCallback = null) => false;
        }
        [HarmonyPatch(typeof(Backtrace.Unity.BacktraceClient), "ShouldSkipReport")]
        internal static class ShouldSkip
        {
            static bool Prefix(ReportFilterType type, Exception exception, string message, bool __result)
            {
                __result = true;
                return false;
            }
        }
    }
}   
