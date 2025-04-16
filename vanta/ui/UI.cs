using System;
using System.Collections.Generic;
using GorillaExtensions;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using vanta.miscellaneous;

namespace vanta.wrist
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "LateUpdate")]
    internal class UI : MonoBehaviour
    {
        public static GameObject MenuCanvasObject;
        private static Vector3 currentPos;
        private static Quaternion currentRot;
        static void Prefix()
        {
            try
            {
                if (MenuCanvasObject.IsNull())
                {
                    DrawE();
                }
                Smooth();
            }
            catch (Exception e)
            {
                Debug.LogError($"ERROR: {e.Message}. STACK TRACE: {e.StackTrace}");
            }
        }

        /* hides menu from cam or wtv
        HideCamera(MenuCanvasObject, HiddenUILayer);
Camera tpc = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera")?.GetComponent<Camera>();
_ = tpc != null && (tpc.cullingMask &= ~(1 << 30)) >= 0;*/

        static void DrawE()
        {
            MenuCanvasObject = Canvas();
            CreatePanel();
        }

        static GameObject Canvas()
        {
            GameObject canvasObject = new GameObject();
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            var rectTransform = canvasObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(240, 140);
            rectTransform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
            canvasObject.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 23;
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvasObject;
        }

        static GameObject CreatePanel()
        {
            GameObject panel = new GameObject();
            panel.transform.SetParent(MenuCanvasObject.transform, false);
            var rectTransform = panel.AddComponent<RectTransform>();
            rectTransform.sizeDelta = MenuCanvasObject.GetComponent<RectTransform>().sizeDelta;
            rectTransform.anchoredPosition = new Vector2(0, 30);
            var image = panel.AddComponent<Image>();
            image.color = new Color(0f, 0f, 0f, 0.90f);
            return panel;
        }

        static bool ConditionCheck()
        {
            return !MenuCanvasObject.Exists(); /*&& ControllerInputPoller.instance.leftControllerPrimaryButton;*/
        }

        static void Smooth()
        {
            if (Camera.main is not Camera cam || MenuCanvasObject == null) return;
            Vector3 targetPos = cam.transform.position + cam.transform.forward * 0.8f;
            Quaternion targetRot = Quaternion.LookRotation(targetPos - cam.transform.position);
            currentPos = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * 5f);
            currentRot = Quaternion.Slerp(currentRot, targetRot, Time.deltaTime * 5f);
            MenuCanvasObject.transform.position = currentPos;
            MenuCanvasObject.transform.rotation = currentRot;
        }


        // NOT gpt.
        static void HideCamera(GameObject obj, int newLayer)
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
