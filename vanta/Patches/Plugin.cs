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
using vanta.miscellaneous;
using UnityEngine.UI;
using TMPro;
using vanta.wrist;

namespace vanta.patches
{
    [BepInPlugin("com.vanta", "vanta cheat menu", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            new Harmony("vantacheat").PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("init vanta.");
            GameObject go = new GameObject("Vanta");
            go.AddComponent<UI>();
            go.AddComponent<RPCProtection>();
            DontDestroyOnLoad(go);
        }
    }
}
