﻿using GameNetcodeStuff;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardTime.Scripts;

namespace WizardTime
{
    internal class MonomodPatches
    {
        #region Networking
        public static void GameNetworkManagerPatch(Action<GameNetworkManager> orig, GameNetworkManager self)
        {
            orig(self);
            NetworkManager.Singleton.AddNetworkPrefab(WizardTimePlugin.focusOrb);
            foreach(Spell spell in AssetSummoner.spells)
            {
                if(spell.SpellPrefab != null && spell.SpellPrefab.TryGetComponent(out NetworkObject netObj))
                {
                    NetworkManager.Singleton.AddNetworkPrefab(spell.SpellPrefab);
                }
            }
        }
        public static void StartOfRoundAwake(Action<StartOfRound> orig, StartOfRound self)
        {
            orig(self);
                    WizardTimePlugin.mls.LogInfo("doing stuff");
            if(NetworkManager.Singleton.IsHost ||  NetworkManager.Singleton.IsServer)
            {
                if(WizardTimePlugin.focusOrb != null)
                {
                    GameObject temp = UnityEngine.Object.Instantiate(WizardTimePlugin.focusOrb);
                    if(temp.TryGetComponent(out NetworkObject netobj))
                    {
                        if (!netobj.IsSpawned)
                        {
                            netobj.Spawn();
                            WizardTimePlugin.mls.LogInfo("spawning");
                        }
                        else
                        {
                            WizardTimePlugin.mls.LogInfo(netobj.gameObject.name);
                        }
                    }
                }
            }
        }
        #endregion
        #region Player stuff
        public static void PlayerStartPatch(Action<PlayerControllerB> orig, PlayerControllerB self)
        {
            orig(self);
            //self.gameObject.AddComponent<SpellBook>();
        }
        public static void ActivateItem_performedPatch(Action<PlayerControllerB, InputAction.CallbackContext> orig, PlayerControllerB self, InputAction.CallbackContext context)
        {
            SpellBook spellBookInstance = SpellBook.Instance;
            if (self == StartOfRound.Instance.localPlayerController)
            {
                if (spellBookInstance != null)
                {
                    spellBookInstance.CastSpellServerRpc(self.gameObject);
                }
                orig(self,context);
            }
        }
        #endregion
    }

}
