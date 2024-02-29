using BepInEx.Logging;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardTime.Scripts;

namespace WizardTime
{
    internal class MonomodPatches
    {
        public static void PlayerStartPatch(Action<PlayerControllerB> orig, PlayerControllerB self)
        {
            orig(self);
            self.gameObject.AddComponent<SpellBook>();
        }
        public static void ActivateItem_performedPatch(Action<PlayerControllerB, InputAction.CallbackContext> orig, PlayerControllerB self, InputAction.CallbackContext context)
        {
            if(self == StartOfRound.Instance.localPlayerController)
            {
                if(self.TryGetComponent(out SpellBook spellBook))
                {
                    if (spellBook.selectedTome != null && spellBook.selectedTome.selectedSpell != null)
                    {
                        spellBook.selectedTome.CastSpell(spellBook.selectedTome.selectedSpell);
                        spellBook.selectedTome.selectedSpell.Cast();
                    }
                }
                orig(self,context);
            }
        }
    }
    
}
