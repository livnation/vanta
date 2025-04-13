using System;
using System.Collections;
using System.Net;
using BepInEx;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace vanta.miscellaneous
{
    [BepInPlugin("com.vanta.music", "vanta music loader", "1.0")]
    public class PlayMusicStuff : BaseUnityPlugin
    {
        private AudioSource audioSource;
        string[] songs = new string[]
        {
            "https://github.com/livnation/vanta-urls/raw/refs/heads/main/idk.mp3",
            "https://github.com/livnation/vanta-urls/raw/refs/heads/main/idk2.mp3",
            "https://github.com/livnation/vanta-urls/raw/refs/heads/main/mango.mp3"
        };

        string GetStatus()
        {
            try
            {
                return new WebClient().DownloadString("https://github.com/livnation/vanta-urls/raw/refs/heads/main/status.txt");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
            return null;
        }

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            int song = new System.Random().Next(0, songs.Length);
            string url = songs[song];
            Setup(url);
            Debug.Log($"Playing {url}");
            var b = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").GetComponent<TextMeshPro>();
            var c = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").GetComponent<TextMeshPro>();
            var d = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/keyboard (1)").GetComponent<MeshRenderer>();
            var e = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor").GetComponent<MeshRenderer>();
            var f = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen");
            b.text = $"THANK YOU FOR USING VANTA! \n\nDETECTED? {GetStatus()}\nTIME OF STARTUP: {DateTime.UtcNow}";
            c.text = "<color=orange>VANTA 0.1</color>";
            d.material.color = Color.black;
            e.material.color = Color.black;
            f.SetActive(false);
        }

        public void Setup(string url)
        {
            StartCoroutine(dap(url));
        }

        private IEnumerator dap(string url)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"this shouldnt happen: {www.error}");
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.clip = clip;
                    audioSource.volume = 0.14f;
                    audioSource.Play();
                }
            }
        }
    }
}