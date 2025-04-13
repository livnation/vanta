using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using vanta.miscellaneous;

namespace vanta.wrist
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "LateUpdate")]
    internal class LWrist : MonoBehaviour
    {
        public static GameObject mainMenu;
        static GameObject baseMenu;
        static GameObject canvasObject;
        static void Prefix()
        {
            try
            {

                if (!ControllerInputPoller.instance.leftControllerSecondaryButton && baseMenu != null) Cleanup();
                if (!ControllerInputPoller.instance.leftControllerSecondaryButton || baseMenu != null) return;
                Draw();
            }
            catch (Exception e)
            {
                Debug.LogError($"ERROR: {e.Message}. STACK TRACE: {e.StackTrace}");
            }
        }

        static void Cleanup()
        {
            if (baseMenu != null)
            {
                UnityEngine.Object.Destroy(baseMenu);
                UnityEngine.Object.Destroy(mainMenu);
                baseMenu = null;
                mainMenu = null;
            }
        }

        void Update()
        {
            Debug.Log("dwadawd");
            if (baseMenu == null) return;
            baseMenu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            baseMenu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
        }

        public static void Draw()
        {
            baseMenu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(baseMenu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(baseMenu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(baseMenu.GetComponent<Renderer>());
            baseMenu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);
            mainMenu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(mainMenu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(mainMenu.GetComponent<BoxCollider>());
            mainMenu.transform.parent = baseMenu.transform;
            mainMenu.transform.rotation = Quaternion.Euler(90, 0, 0);
            mainMenu.transform.localScale = new Vector3(0.01f, 0.8f, 0.8f);
            mainMenu.GetComponent<Renderer>().material.color = Color.blue;
            mainMenu.transform.position = new Vector3(0.05f, 0f, 0f);
            mainMenu.name = "Menu";
            canvasObject = new GameObject();
            canvasObject.transform.parent = baseMenu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
            Color bll;
            Color all;
            ColorUtility.TryParseHtmlString("#CB356B", out bll);
            ColorUtility.TryParseHtmlString("#BD3F32", out all);
            vanta.miscellaneous.Gradient.ApplyToGameObject(mainMenu, bll, all, 0.5f);
            Text text = new GameObject
            {
                transform =//
                {//
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = Font.CreateDynamicFontFromOSFont("MS Gothic", 16);
            text.text = "Vanta";
            text.fontSize = 1;
            text.color = Color.blue;
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.019f);
            component.position = new Vector3(0.06f, 0f, 0.140f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void Recreate()
        {

        }
    }
}
