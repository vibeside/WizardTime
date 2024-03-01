using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using WizardTime.SpellComponents;

namespace WizardTime.Scripts
{
    [CreateAssetMenu(menuName = $"Magic Practices/Spell")]
    internal class Spell : ScriptableObject
    {
        public string Name = "";
        public Magicks SpellType;
        public SpellEffects? spellComponent;
        public float ManaCost;
        public GameObject? SpellPrefab;
        public Spell(string name, SpellEffects magic, float manacost, Magicks type, GameObject? prefab = null) 
        {
            Name = name;
            spellComponent = magic;
            ManaCost = manacost;
            SpellPrefab = prefab;
            SpellType = type;
        }
    }
}
