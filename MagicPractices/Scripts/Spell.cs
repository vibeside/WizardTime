using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.Scripts
{
    [CreateAssetMenu(menuName = $"Magic Practices/Spell")]
    public class Spell : ScriptableObject
    {
        public string Name = "";
        public delegate void Effects();
        public Magicks SpellType;
        public Effects SpellEffects;
        public float ManaCost;
        public GameObject? SpellPrefab;
        public Spell(string name, Effects magic, float manacost, Magicks type, GameObject? prefab = null) 
        {
            Name = name;
            SpellEffects = magic;
            ManaCost = manacost;
            SpellPrefab = prefab;
            SpellType = type;
        }
    }
}
