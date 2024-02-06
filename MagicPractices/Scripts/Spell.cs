using System;
using System.Collections.Generic;
using System.Text;

namespace WizardTime.Scripts
{
    internal class Spell
    {
        public string Name { get; set; } = "";
        public delegate void Effects();
        public Effects ConjuredMagic;
        public float ManaCost;
        public Spell(string name, Effects magic, float cost) 
        {
            Name = name;
            ConjuredMagic = magic;
            ManaCost = cost;
        }
    }
}
