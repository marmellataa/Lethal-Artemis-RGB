using HarmonyLib;
using UnityEngine;
using GameNetcodeStuff;

namespace LCArtemis
{

    [HarmonyPatch(typeof(PlayerControllerB), "Emote1_performed")]
    public class EmotePatch
    {
        static void Postfix()
        {
            RGBService.TriggerRainbow();
        }
    }

}