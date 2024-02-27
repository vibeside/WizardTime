using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Tome : MonoBehaviour
    {
        public Spell? minorMagicks;
        public Spell? majorMagicks;
        public Spell? buffMagicks;
        public Spell? defensiveMagicks;
        public Spell? selectedSpell;
        public virtual void CastSpell(Spell castedSpell)
        {
            castedSpell.SpellEffects();
        }
    }
}
