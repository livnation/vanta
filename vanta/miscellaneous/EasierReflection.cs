using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace vanta.miscellaneous
{
    internal class EasierReflection
    {
        public static EasierReflection instance = new EasierReflection();
        
        public void GrabAndInvoke(Type cls, string method, params object[] prms)
        {
            var info = cls.GetMethod(method);
            object target = null;
            if (info == null) return;
            if (!info.IsStatic) target = Activator.CreateInstance(cls);
            info.Invoke(target, prms);
        }
    }
}
