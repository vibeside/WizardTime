using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.SpellComponents
{
    internal class SpellEffects : MonoBehaviour
    {
        public virtual void MoveToLocation(Vector3 location)
        {
            transform.position = location;
        }
    }
}
