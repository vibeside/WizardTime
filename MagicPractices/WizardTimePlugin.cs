using BepInEx;
using BepInEx.Logging;
using System;
using UnityEngine;

namespace WizardTime
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WizardTimePlugin : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.WizardTime";
        public const string modName = "MagicPracices";
        public const string modVersion = "0.1.0.0";
        public static ManualLogSource? mls;
        public static GameObject? focusOrb;
        public void Awake()
        {
            mls = base.Logger;
            //Holds the scripts used by the mod to do magic!
            focusOrb = new GameObject("Magical Glowing Orb");

        }
    }
}
