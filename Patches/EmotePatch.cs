using HarmonyLib;
using GameNetcodeStuff;

namespace LCArtemis
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    public class EmotePatch
    {
        private static bool wasDancing = false;

        static void Postfix(PlayerControllerB __instance)
        {
            if (!__instance.isPlayerControlled || !__instance.IsOwner) return;

            bool isDancing = __instance.performingEmote;

            if (isDancing != wasDancing)
            {
                wasDancing = isDancing; // Aggiorna la memoria

                // Invia il segnale ad Artemis per accendere o spegnere i LED
                RGBService.SetDancingState(isDancing);
            }
        }
    }
}