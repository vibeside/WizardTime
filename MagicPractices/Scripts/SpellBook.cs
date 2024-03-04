using BepInEx;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using WizardTime.SpellComponents;

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
            if(mana <= 0 ) mana = 0;
        }
        [ServerRpc(RequireOwnership = false)]
        public void DamagePlayerFromCasterServerRpc(NetworkBehaviourReference damagedPlayer,NetworkBehaviourReference caster, int damage)
        {
            DamagePlayerFromCasterClientRpc(damagedPlayer, caster,damage);
        }
        [ClientRpc]
        public void DamagePlayerFromCasterClientRpc(NetworkBehaviourReference damagedPlayer, NetworkBehaviourReference caster, int damage)
        {
            if (damagedPlayer.TryGet(out PlayerControllerB player) && caster.TryGet(out PlayerControllerB wizard))
            {
                DamagePlayerFromCaster(player,wizard,damage);
            }
            else
            {
                WizardTimePlugin.mls.LogInfo($"Damage player method given wrong parameters!{nameof(damagedPlayer)} and {nameof(caster)}");
            }
        }
        public void DamagePlayerFromCaster(PlayerControllerB damagedPlayer, PlayerControllerB caster, int damage)
        {
            damagedPlayer.health -= damage;
        }
        [ServerRpc(RequireOwnership = false)]
        public void CastSpellServerRpc(NetworkBehaviourReference caster)
        {
            if (!caster.TryGet(out PlayerControllerB wizard))
                throw new ArgumentException(nameof(caster), "this mf ain't no wizard");
            CastSpell(wizard);
        }
        public void CastSpell(PlayerControllerB caster)
        {
            if (caster.performingEmote) return;
            if (selectedTome != null && selectedTome.selectedSpell != null && selectedTome.selectedSpell.SpellPrefab != null)
            {
                if (IsServer || IsHost)
                {
                    GameObject spellPrefab = Instantiate(selectedTome.selectedSpell.SpellPrefab, caster.gameplayCamera.transform.position, caster.gameplayCamera.transform.rotation);
                    if (spellPrefab.TryGetComponent(out SpellEffects spelleffects))
                    {
                        spelleffects.caster = caster;
                    }
                    if (spellPrefab.TryGetComponent(out NetworkObject netobj))
                    {
                        netobj.Spawn();
                    }
                }
            }
        }
    }
}
