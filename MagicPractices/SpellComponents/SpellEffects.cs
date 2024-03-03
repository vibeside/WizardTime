using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.SpellComponents
{
    internal class SpellEffects : NetworkBehaviour
    {
        public PlayerControllerB? caster;
        public virtual void MoveToLocation(Vector3 location)
        {
            transform.position = location;
        }
        public virtual void Awake()
        {
            if(caster == null)
            {
                caster = StartOfRound.Instance.localPlayerController;
            }
        }
    }
}
