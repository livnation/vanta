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
        // This does NOT prevent bans, only prevents local reports such as speed boost detection
        [HarmonyPatch(typeof(GorillaNot), "SendReport")]
        static class SRP
        {
            static bool Prefix(ref string susReason, ref string susId, ref string susNick)
            {
                susReason = susId = susNick = null;
                return false;
            }
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
