using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace vanta.buttons
{
    public class ModManager
    {
        public enum Categories
        {
            Main,
            Movement,
            Master,
            Overpowered,
            Visuals
        }

        public static Lazy<List<BaseMod>> Mods = new Lazy<List<BaseMod>>(GetMods);


        public static BaseMod AddMod(Type mod)
        {
            GameObject gameObject = new GameObject($"VantaMod_{mod.Name}");

            return gameObject.AddComponent(mod) as BaseMod;
        }

        public static List<BaseMod> GetMods()
        {
            var moduleTypes = Assembly.GetExecutingAssembly().GetTypes()
                             .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseMod)))
                             .ToList();


            Debug.Log("Found " + moduleTypes.Count + " modules");

            List<BaseMod> mods = new List<BaseMod>();

            foreach (var type in moduleTypes)
            {
                try
                {
                    Debug.Log("Attempt to add mod: " + type.Name);

                    BaseMod mod = AddMod(type);

                    mods.Add(mod);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Could not load mod: " + type.Name + " " + ex.Message);
                }
            }

            Debug.Log("Loaded " + mods.Count + " modules");

            return mods;
        }
    }
}
