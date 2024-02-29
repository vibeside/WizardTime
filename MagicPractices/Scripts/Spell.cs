using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Spell : ScriptableObject
    {
        public string Name = "";
        public delegate void Effects();
        public Effects SpellEffects;
        public float ManaCost;
        public Spell(string name, Effects magic, float manacost) 
        {
            Name = name;
            SpellEffects = magic;
            ManaCost = manacost;
        }
        public void Cast()
        {
            SpellEffects();
        }
    }
}
