using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.Scripts
{
    [CreateAssetMenu(menuName = $"Magic Practices/Tome")]
    internal class Tome : ScriptableObject
    {
        public Spell? minorMagicks;
        public Spell? majorMagicks;
        public Spell? buffMagicks;
        public Spell? defensiveMagicks;
        public Spell? selectedSpell;
        public Magicks MagicksType;
        public bool Unlocked = false;
    }  
}
