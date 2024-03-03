using Discord;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UIElements.Experimental;
using UnityEngine.VFX;
using System.Collections;

namespace WizardTime.SpellComponents
{
    internal class FireBolt : SpellEffects
    {
        public Transform? target;
        public float rotationSpeed = 1f;
        public float speed = 3f;
        public Rigidbody? self;
        private Vector3 direction = Vector3.zero;
        private Quaternion lookTo;
        private bool hitSomething = false;
        public VisualEffect FireTrail = null!;
        public VisualEffect FireBurst = null!;
        public void Awake()
        {
            gameObject.layer = 7;
            speed = 15f;
            Destroy(gameObject, 500);
            //557520767
            FireBurst.Stop();
            StartCoroutine(waitXSecondsThenTest(0.01f));
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
            EnemyAI? enemy = other.GetComponentInParent<EnemyAI>();
            if ((557520767 & (1 << other.gameObject.layer)) > 0 && (!other.isTrigger || enemy != null))
            {
                PlayerControllerB? player = other.GetComponentInParent<PlayerControllerB>();
                if ((player != null && 
                    player != StartOfRound.Instance.localPlayerController) ||
                    player == null)
                {
                    hitSomething = true;
                    FireTrail.Stop();
                    FireBurst.Play();
                    if(enemy != null)
                    {
                        enemy.HitEnemyServerRpc(1,0,true);
                    }
                }
            }
        }
        public IEnumerator waitXSecondsThenTest(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, hitInfo: out hit, Mathf.Infinity, 557520767))
            {
                EnemyAI? enemy = hit.transform.GetComponentInParent<EnemyAI>();
                if (enemy != null)
                {
                    ScanNodeProperties? scanNode = enemy.GetComponentInChildren<ScanNodeProperties>();
                    WizardTimePlugin.mls.LogInfo($"{enemy.enemyType.name}");
                    target = scanNode!.transform;
                }
            }
        }
        public override void OnDestroy()
        {
            FireTrail.Stop();
            FireBurst.Play();
            base.OnDestroy();
        }
    }
}
