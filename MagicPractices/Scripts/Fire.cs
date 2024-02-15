using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Fire : MonoBehaviour
    {
        public static Spell Fireball = new Spell("Fireball",FireballMagicks,10f);
        public static Spell Explosion;
        public static float temptimer = 0f;
        public void Awake()
        {

        }
        public void Update()
        {
            temptimer += Time.deltaTime;
        }
        public static void FireballMagicks()
        {
            GameObject fireball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fireball.GetComponent<Renderer>().material.shader = Shader.Find("HDRP/Lit");
        }
    }
}
