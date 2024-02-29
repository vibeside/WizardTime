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
        public Fire? fireKnowledge;
        public Blood? bloodKnowledge;
        public Thunder? thunderKnowledge;
        public Gravity? gravityKnowledge;
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
            //fireKnowledge = GetComponent<Fire>();
            //selectedTome = fireKnowledge;
            //selectedTome.selectedSpell = fireKnowledge.minorMagicks;
            //WizardTimePlugin.mls.LogInfo(fireKnowledge == null);
            //WizardTimePlugin.mls.LogInfo(selectedTome.minorMagicks == null);
            //WizardTimePlugin.mls.LogInfo(selectedTome.selectedSpell == null);

        }
        public void Update()
        {
            if (localPlayer == null && StartOfRound.Instance != null) localPlayer = StartOfRound.Instance.localPlayerController;
            if (mana < 100)
            {
                mana += Time.deltaTime * manaRegenSpeed;
            }
            else
            {
                mana = 100f;
            }
        }
        public static void testmore()
        {
            Instance.TestServerRpc();
        }
        [ServerRpc(RequireOwnership = false)]
        public void TestServerRpc()
        {
            TestClientRpc();
        }
        [ClientRpc]
        public void TestClientRpc()
        {
            WizardTimePlugin.mls.LogInfo("A");
        }
        
    }
}
