using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.Scripts.Animation
{
    internal class AnimationController : NetworkBehaviour
    {
        public void Awake()
        {
        }
        [ServerRpc]
        public void PlayAnimationServerRpc()
        {

        }
        [ClientRpc]
        public void PlayAnimationClientRpc()
        {

        }
    }
}
