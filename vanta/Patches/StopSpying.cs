using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backtrace.Unity.Model;
using Backtrace.Unity.Types;
using HarmonyLib;

namespace vanta.Patches
{
    internal static class StopSpying
    {
        [HarmonyPatch(typeof(Backtrace.Unity.BacktraceClient), "SendReport")]
        internal class ReportData
        {
            static bool Prefix(BacktraceReport report, Action<BacktraceResult> sendCallback = null)
            {
                return false;
            }
        }
        [HarmonyPatch(typeof(Backtrace.Unity.BacktraceClient), "ShouldSkipReport")]
        internal static class ShouldSend
        {//
            static bool Prefix(ReportFilterType type, Exception exception, string message, bool __result)
            {
                 /*If you're wondering, this essentially makes the method ALWAYS return true and makes
                it so the original method does NOT run. */
                __result = true;
                return false;
            }
        }
    }
}   
