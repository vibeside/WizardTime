using GameNetcodeStuff;
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
        public float speed = 1f;
        public Rigidbody? self;
        private Vector3 direction = Vector3.zero;
        private Quaternion lookTo;
        private bool hitSomething = false;
        private LayerMask layermask = 11012424;
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
                transform.rotation = Quaternion.Slerp(transform.rotation, lookTo, Time.deltaTime * rotationSpeed);
            }
            if (self != null)
            {
                if (!hitSomething) self.velocity = transform.forward;
                if (hitSomething) self.velocity = Vector3.zero;
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if ((layermask.value & (1 << other.gameObject.layer)) > 0)
            {
                //11012424
                WizardTimePlugin.mls.LogInfo($"hit something {other.gameObject.name}");
            }
        }
    }
}
