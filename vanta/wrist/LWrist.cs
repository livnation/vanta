using System;
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

        private const int HiddenUILayer = 30; // Use a free layer index

        static void Prefix()
        {
            try
            {
                if (MenuCanvasObject == null)
                {
                    DrawE();
                }

                SmoothFollowCamera();
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

            var rect = MenuCanvasObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(400, 600);
            rect.localScale = Vector3.one * 0.0030f;

            MenuCanvasObject.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10;
            MenuCanvasObject.AddComponent<GraphicRaycaster>();

            // Background Panel
            GameObject panelGO = new GameObject("Background");
            panelGO.transform.SetParent(MenuCanvasObject.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.sizeDelta = rect.sizeDelta;

            var img = panelGO.AddComponent<Image>();
            img.color = new Color(0f, 0f, 0f, 0.85f);

            // Assign the hidden layer recursively
            SetLayerRecursively(MenuCanvasObject, HiddenUILayer);

            // Hide from third-person recording cam
            Camera thirdPersonCam = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera")?.GetComponent<Camera>();
            if (thirdPersonCam != null)
            {
                thirdPersonCam.cullingMask &= ~(1 << HiddenUILayer);
            }

            // Initialize smooth values
            currentPos = MenuCanvasObject.transform.position;
            currentRot = MenuCanvasObject.transform.rotation;
        }

        static void SmoothFollowCamera()
        {
            if (MenuCanvasObject == null) return;

            Camera cam = Camera.main;
            if (cam == null) return;

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
            obj.layer = newLayer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}
