using BepInEx.Logging;
using UnityEngine;

namespace LCArtemis
{
    public static class RGBService
    {
        public static void TriggerRainbow()
        {
            LCArtemis.Logger.LogInfo("🎨 Emote1 → Rainbow triggered");

            // TEST 1: conferma che l'hook funziona
            Debug.Log("RGB EVENT: RAINBOW MODE");
        }
    }
}