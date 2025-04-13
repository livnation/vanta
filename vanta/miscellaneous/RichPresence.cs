using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace vanta.miscellaneous
{
    internal static class RichPresence
    {
        // Someone sent me this, I know its Steal, so credits to them.
        public static void Init()
        {
            rpcAssembly = Assembly.Load(LoadEmbeddedResource("vanta.resources.DiscordRPC.dll"));
            Type type = rpcAssembly.GetType("DiscordRPC.DiscordRpcClient");
            object obj = Activator.CreateInstance(type, new object[] { "1361052851576508617" });
            type.GetMethod("Initialize").Invoke(obj, null);
            Type type2 = rpcAssembly.GetType("DiscordRPC.RichPresence");
            object obj2 = Activator.CreateInstance(type2);
            type2.GetProperty("Details").SetValue(obj2, "Using Vanta.");
            type2.GetProperty("State").SetValue(obj2, "https://discord.gg/ae55n7gMgA");
            Type type3 = rpcAssembly.GetType("DiscordRPC.Assets");
            object obj3 = Activator.CreateInstance(type3);
            type2.GetProperty("Assets").SetValue(obj2, obj3);
            type.GetMethod("SetPresence").Invoke(obj, new object[] { obj2 });
        }
        private static byte[] LoadEmbeddedResource(string resourceName)
        {
            byte[] array;
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (manifestResourceStream == null)
                {
                    throw new ArgumentException("Resource '" + resourceName + "' not found.");
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    manifestResourceStream.CopyTo(memoryStream);
                    array = memoryStream.ToArray();
                }
            }
            return array;
        }
        private static Assembly rpcAssembly;
    }
}
