using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Fire : Tome
    {
        public GameObject? fireballPrefab;
        public void Awake()
        {
            if (WizardTimePlugin.magicksAssets != null)
            {
                fireballPrefab = WizardTimePlugin.magicksAssets.LoadAsset<GameObject>("Assets/magicks/Fireball.prefab");
            }
            minorMagicks = new Spell("Fireball", FireballMagicks, 1f);
        }
        public override void CastSpell(Spell castedSpell)
        {
            base.CastSpell(castedSpell);
        }
        public void FireballMagicks()
        {
            if (fireballPrefab == null)
            {
                WizardTimePlugin.mls.LogError("Warning, Fireball Prefab didn't load");
                return;
            }
            GameObject ballOfFire = Instantiate(fireballPrefab);
            ballOfFire.transform.position = StartOfRound.Instance.localPlayerController.gameplayCamera.transform.position;
            //ballOfFire.transform.rotation = Quaternion.Euler(StartOfRound.Instance.localPlayerController.gameplayCamera.transform.forward);
            ballOfFire.transform.up = -StartOfRound.Instance.localPlayerController.gameplayCamera.transform.forward;

        }
    }
}
