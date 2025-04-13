using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace vanta.miscellaneous
{
    internal class Gradient : MonoBehaviour
    {
        public Color topLeftColor = Color.cyan;
        public Color bottomRightColor = Color.magenta;
        public float flowSpeed = 0.5f;
        public int textureSize = 128;
        private Material gradientMaterial;
        private Texture2D gradientTexture;
        private float animationProgress = 0f;
        private int frameCount = 0;
        private int updateFrequency = 2;
        private Color[] pixels;
        public static void ApplyToGameObject(GameObject target, Color topLeftColor, Color bottomRightColor, float flowSpeed = 0.5f)
        {
            Gradient gradient = target.AddComponent<Gradient>();
            gradient.topLeftColor = topLeftColor;
            gradient.bottomRightColor = bottomRightColor;
            gradient.flowSpeed = flowSpeed;
            gradient.Initialize();
        }
        private void Initialize()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer == null) return;
            gradientTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
            gradientTexture.wrapMode = TextureWrapMode.Clamp;
            gradientMaterial = new Material(Shader.Find("Unlit/Texture"));
            gradientMaterial.mainTexture = gradientTexture;
            renderer.material = gradientMaterial;
            pixels = new Color[textureSize * textureSize];
            UpdateGradientTexture();
        }

        private void UpdateGradientTexture()
        {
            if (frameCount % updateFrequency == 0)
            {
                for (int y = 0; y < textureSize; y++)
                {
                    for (int x = 0; x < textureSize; x++)
                    {
                        float u = (float)x / (textureSize - 1);
                        float v = (float)y / (textureSize - 1);
                        float t = Mathf.PingPong((u + v) / 2 + animationProgress, 1f);
                        t = SmoothStep(t);
                        Color gradientColor = Color.Lerp(topLeftColor, bottomRightColor, t);
                        pixels[y * textureSize + x] = gradientColor;
                    }
                }
                gradientTexture.SetPixels(pixels);
                gradientTexture.Apply();
            }
            frameCount++;
        }

        private float SmoothStep(float t)
        {
            return t * t * (3f - 2f * t);
        }

        private void Update()
        {
            animationProgress += Time.deltaTime * flowSpeed;
            UpdateGradientTexture();
        }
    }
}
