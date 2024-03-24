using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts.Animation
{
    internal class BoneConstraint : MonoBehaviour
    {
        public Transform? sourceBone;
        public Transform? targetBone;
        public void Awake()
        {
            targetBone = GetComponent<Transform>();
            if (sourceBone == null)
            {
                WizardTimePlugin.mls.LogInfo($"Source bone = null, u fucked up. destroying constraint {targetBone.name}");
                Destroy(this);
            }
        }
    }
}
