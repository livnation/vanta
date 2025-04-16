using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace vanta.patches
{
    static internal class PreventLocalReporting
    {
        [HarmonyPatch(typeof(GorillaNot), "SendReport")]
        static class SRP
        {
            static bool Prefix() => false;
        }
        [HarmonyPatch(typeof(GorillaNot), "CheckReports")]
        static class CRP
        {
            static bool Prefix() => false;
        }
        [HarmonyPatch(typeof(GorillaNot), "DispatchReport")]
        static class DRP
        {
            static bool Prefix() => false;
        }
    }
}
