using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace vanta.wrist
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "LateUpdate")]
    internal class LWrist : MonoBehaviour
    {
        public static GameObject MenuCanvasObject;
        private static Vector3 currentPos;
        private static Quaternion currentRot;
        private const int HiddenUILayer = 30;
        static void Prefix()
        {
            try
            {
                //l5ds add input later
                if (MenuCanvasObject == null) DrawE();
                Smooth();
            }
            catch (Exception e)
            {
                Debug.LogError($"ERROR: {e.Message}. STACK TRACE: {e.StackTrace}");
            }
        }

        static void DrawE()
        {
            MenuCanvasObject = new GameObject("FloatingMenuCanvas");
            var canvas = MenuCanvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            RectTransform rectum = MenuCanvasObject.GetComponent<RectTransform>();
            rectum.sizeDelta = new Vector2(400, 600);
            rectum.localScale = Vector3.one + new Vector3(2f, 2f, 2f) * 0.0030f;
            MenuCanvasObject.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10;
            MenuCanvasObject.AddComponent<GraphicRaycaster>();
            GameObject mainPanel = new GameObject("Background");
            mainPanel.transform.SetParent(MenuCanvasObject.transform, false);
            RectTransform panelrectum = mainPanel.AddComponent<RectTransform>();
            panelrectum.sizeDelta = rectum.sizeDelta;
            var img = mainPanel.AddComponent<Image>();
            img.color = new Color(0f, 0f, 0f, 0.85f);
            SetLayerRecursively(MenuCanvasObject, 30);
            Camera tpc = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera")?.GetComponent<Camera>();
            if (tpc != null) tpc.cullingMask &= ~(1 << 30);
            currentPos = MenuCanvasObject.transform.position;
            currentRot = MenuCanvasObject.transform.rotation;
        }

        static void Smooth()
        {
            if (MenuCanvasObject == null) return;
            Camera cam = Camera.main;
            if (cam == null) return;
            // positioning is from gpt idgaf
            Vector3 targetPos = cam.transform.position + cam.transform.forward * 2.5f;
            Quaternion targetRot = Quaternion.LookRotation(targetPos - cam.transform.position);
            currentPos = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * 5f);
            currentRot = Quaternion.Slerp(currentRot, targetRot, Time.deltaTime * 5f);
            MenuCanvasObject.transform.position = currentPos;
            MenuCanvasObject.transform.rotation = currentRot;
        }

        static void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (obj == null) return;
            var stack = new Stack<Transform>();
            stack.Push(obj.transform);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                current.gameObject.layer = newLayer;
                foreach (Transform child in current)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
