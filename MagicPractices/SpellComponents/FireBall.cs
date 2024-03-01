using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.SpellComponents
{
    internal class FireBall : SpellEffects
    {
        public Transform? target;
        public float rotationSpeed = 1f;
        private Vector3 direction = Vector3.zero;
        private Quaternion lookTo;
        public void Awake()
        {
            WizardTimePlugin.mls.LogInfo("It's wizard time motherfucker! I cast fireball!");
        }
        public void Update()
        {
            if (target != null)
            {
                direction = (target.position - transform.position).normalized;
                lookTo = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation,lookTo,Time.deltaTime*rotationSpeed);
            }
        }
    }
}
