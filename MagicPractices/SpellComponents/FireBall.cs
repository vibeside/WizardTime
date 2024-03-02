using Discord;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UIElements.Experimental;

namespace WizardTime.SpellComponents
{
    internal class FireBall : SpellEffects
    {
        public Transform? target;
        public float rotationSpeed = 1f;
        public float speed = 3f;
        public Rigidbody? self;
        private Vector3 direction = Vector3.zero;
        private Quaternion lookTo;
        private bool hitSomething = false;
        public void Awake()
        {
            WizardTimePlugin.mls.LogInfo("It's wizard time motherfucker! I cast fireball!");
            Destroy(gameObject, 5);
            //557520767
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
                if (!hitSomething && !self.isKinematic) self.velocity = transform.forward * speed;
                if (hitSomething && !self.isKinematic) self.velocity = Vector3.zero;
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if ((557520767 & (1 << other.gameObject.layer)) > 0)
            {
                if (other.GetComponentInParent<PlayerControllerB>() != null && 
                    other.GetComponentInParent<PlayerControllerB>() != StartOfRound.Instance.localPlayerController)
                {
                    WizardTimePlugin.mls.LogInfo($"{other.gameObject.name} was hit by fireball");
                }
                if (other.GetComponentInParent<PlayerControllerB>() == null)
                {
                    WizardTimePlugin.mls.LogInfo($"{other.gameObject.name} was hit by fireball");
                }
            }
        }
    }
}
