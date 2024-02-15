using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;
using WizardTime.Scripts;

namespace WizardTime
{
    internal class MonomodPatches
    {
        public static void ActivateItem_performedPatch(Action<PlayerControllerB, InputAction.CallbackContext> orig, PlayerControllerB self, InputAction.CallbackContext context)
        {
            if(self == StartOfRound.Instance.localPlayerController)
            {

            }
        }
    }
}
