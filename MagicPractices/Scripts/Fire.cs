using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Unity.Netcode;

namespace WizardTime.Scripts
{
    internal class Fire : Tome
    {
        
        public void Awake()
        {
            minorMagicks = new Spell("Fireball", FireballMagicks, 1f, Magicks.Fire);
            selectedSpell = minorMagicks;
        }
        //public override void CastSpell()
        //{
        //    base.CastSpell();
        //}
        public void FireballMagicks()
        {
            //if (fireballPrefab == null)
            //{
            //    WizardTimePlugin.mls.LogError("Warning, Fireball Prefab didn't load");
            //    return;
            //}
            //GameObject ballOfFire = Instantiate(fireballPrefab);
            //if (ballOfFire.TryGetComponent(out NetworkObject netObj)) netObj.Spawn();
            //ballOfFire.transform.position = transform.position;
            //ballOfFire.transform.up = -transform.forward;

        }
    }
}
