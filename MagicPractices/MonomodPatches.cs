using GameNetcodeStuff;
using System;
using Unity.Netcode;
using UnityEngine.InputSystem;
using WizardTime.Scripts;

namespace WizardTime
{
    internal class MonomodPatches
    {
        public static void GameNetworkManagerPatch(Action<GameNetworkManager> orig, GameNetworkManager self)
        {
            orig(self);
            NetworkManager.Singleton.AddNetworkPrefab(WizardTimePlugin.focusOrb);
        }
        public static void PlayerStartPatch(Action<PlayerControllerB> orig, PlayerControllerB self)
        {
            orig(self);
            //self.gameObject.AddComponent<SpellBook>();
        }
        public static void ActivateItem_performedPatch(Action<PlayerControllerB, InputAction.CallbackContext> orig, PlayerControllerB self, InputAction.CallbackContext context)
        {
            SpellBook spellBookInstance = SpellBook.Instance;
            if(self == StartOfRound.Instance.localPlayerController)
            {
                if(spellBookInstance.selectedTome != null && spellBookInstance.selectedTome.selectedSpell != null)
                {
                    if(!(spellBookInstance.selectedTome.selectedSpell.ManaCost > spellBookInstance.mana))
                    {
                        spellBookInstance.selectedTome.CastSpellOnServerRpc();
                    }
                    else
                    {
                        WizardTimePlugin.mls.LogInfo("No mana!");
                    }
                }
                orig(self,context);
            }
        }
    }
    
}
