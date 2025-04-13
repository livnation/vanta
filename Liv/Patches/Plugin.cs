using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Harmony;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Liv.Patches
{
    [BepInPlugin("com.liv", "Liv Cheat Menu", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("liv").PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("Initialized LIV.");
        }
    }
}
