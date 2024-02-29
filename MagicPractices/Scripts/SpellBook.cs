using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class SpellBook : MonoBehaviour
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
            if(Instance != null)
            {
                DestroyImmediate(Instance);
            }
            Instance = this;
            fireKnowledge = GetComponent<Fire>();
            selectedTome = fireKnowledge;
            selectedTome.selectedSpell = selectedTome.minorMagicks;
        }
        public void Update()
        {
            if(localPlayer == null) localPlayer = StartOfRound.Instance.localPlayerController;
            if (mana < 100)
            {
                mana += Time.deltaTime * manaRegenSpeed;
            }
            else
            {
                mana = 100f;
            }
        }
    }
}
