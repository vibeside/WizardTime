using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class SpellBook : MonoBehaviour
    {
        public bool Unlocked = true;
        public Fire? fireKnowledge;
        public Blood? bloodKnowledge;
        public Thunder? thunderKnowledge;
        public Gravity? gravityKnowledge;
        public Tome? selectedTome;
    }
}
