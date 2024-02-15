using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Fire : MonoBehaviour
    {
        public void Awake()
        {

        }
        public static void FireballMagicks()
        {
            GameObject fireball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fireball.GetComponent<Renderer>().material.shader = Shader.Find("HDRP/Lit");
        }
    }
}
