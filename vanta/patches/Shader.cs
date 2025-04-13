using System;
using HarmonyLib;
using UnityEngine;

namespace vanta.patches
{
    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("CreatePrimitive")]
    internal class Shaders : MonoBehaviour
    {
        private static void Postfix(GameObject __result)
        {
            __result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            __result.GetComponent<Renderer>().material.color = new Color32(255, 128, 0, 128);
        }
    }
}
