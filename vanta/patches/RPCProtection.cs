using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using vanta.miscellaneous;

namespace vanta.patches
{
    internal class RPCProtection : MonoBehaviour
    {
        static  float br = 1f;
        void Update()
        {
            if (!PhotonNetwork.InRoom || Time.time < br) return;
            Debug.Log("Protected");
            Prevention();
            br = Time.time + 2f;
        }

        static void Prevention()
        {
            PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
            EasierReflection.instance.GrabAndInvoke(typeof(GorillaNot), "RefreshRPCs", null);
        }
    }
}
