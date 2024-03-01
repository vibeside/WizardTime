using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class SpellBook : NetworkBehaviour
    {
        public static SpellBook Instance = null!;
        public PlayerControllerB? localPlayer;
        public bool Unlocked = true;
        public Tome? fireKnowledge;
        public Tome? gravityKnowledge;
        public Tome? bloodKnowledge;
        public Tome? thunderKnowledge;
        public Tome? selectedTome;
        public float mana = 100f;
        public float manaRegenSpeed = 1f;
        public void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(Instance);
            }
            Instance = this;
            fireKnowledge = AssetSummoner.tomes[0];
            selectedTome = fireKnowledge;
            selectedTome.selectedSpell = selectedTome.minorMagicks;
        }
        public void Update()
        {
            if (localPlayer == null && StartOfRound.Instance != null) localPlayer = StartOfRound.Instance.localPlayerController;
            if (localPlayer != null)
            {
                transform.position = localPlayer.gameplayCamera.transform.position;
                transform.forward = localPlayer.gameplayCamera.transform.forward;
            }
            if (mana < 100)
            {
                mana += Time.deltaTime * manaRegenSpeed;
            }
            else
            {
                mana = 100f;
            }
        }
        [ServerRpc(RequireOwnership = false)]
        public void CastSpellServerRpc(NetworkObjectReference caster)
        {
            CastSpellClientRpc(caster);
        }
        [ClientRpc]
        public void CastSpellClientRpc(NetworkObjectReference caster)
        {
            if (caster.TryGet(out NetworkObject netobj))
            {
                if(netobj.TryGetComponent(out PlayerControllerB wizard))
                {
                    CastSpell(wizard);
                }
                else
                {
                    WizardTimePlugin.mls.LogInfo($"Couldn't find player script on {netobj.gameObject}");
                }
            }
            else
            {
                WizardTimePlugin.mls.LogInfo($"Couldn't find networkobject on {caster}");
            }
        }
        public void CastSpell(PlayerControllerB caster)
        {
            if (selectedTome != null && selectedTome.selectedSpell != null && selectedTome.selectedSpell.SpellPrefab != null)
            {
                if (IsServer || IsHost)
                {
                    GameObject spellPrefab = Instantiate(selectedTome.selectedSpell.SpellPrefab, caster.transform.position, Quaternion.identity);
                    if (spellPrefab.TryGetComponent(out NetworkObject netobj))
                    {
                        netobj.Spawn();
                    }
                }
            }
        }
    }
}
