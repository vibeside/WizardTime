using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.SpellComponents
{
    internal class SpellEffects : NetworkBehaviour
    {
        public virtual void MoveToLocation(Vector3 location)
        {
            transform.position = location;
        }
    }
}
