using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace vanta.miscellaneous
{
    public static class ObjectLib
    {
        public static bool Exists(this GameObject obj)
        {
            return obj != null;
        }

        public static void Cleanup(this GameObject obj)
        {
            if (!Exists(obj)) return;
            obj.Destroy();
        }
    }
}
