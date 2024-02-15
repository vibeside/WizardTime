using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardTime.Scripts;

namespace WizardTime
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WizardTimePlugin : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.WizardTime";
        public const string modName = "MagicPractices";
        public const string modVersion = "0.1.0.0";
        public static List<Hook> MMHooks = [];
        public static ManualLogSource mls = null!;
        public static GameObject? focusOrb;
        private (uint, uint, uint, uint) QuadHash(int SALT = 0)
        { // [!code ++]
            Hash128 longHash = new();
            longHash.Append(modGUID);
            longHash.Append(SALT);
            return ((uint)longHash.u64_0, (uint)(longHash.u64_0 >> 32),
                    (uint)longHash.u64_1, (uint)(longHash.u64_1 >> 32));
        }
        public void Awake()
        {
            // dont forgor to make a secret spell to cast "Into Cake"
            // vital
            mls = base.Logger;
            //Holds the scripts used by the mod to do magic!
            focusOrb = new GameObject("Magical Glowing Orb");
            focusOrb.AddComponent<NetworkObject>();
            var (hash, _, _, _) = QuadHash(0);
            focusOrb.GetComponent<NetworkObject>().GlobalObjectIdHash = hash;
            focusOrb.AddComponent<SpellBook>();
            focusOrb.AddComponent<Fire>();
            MMHooks.Add(new(typeof(PlayerControllerB).GetMethod(nameof(PlayerControllerB.ActivateItem_performed), (BindingFlags)~0),
            typeof(MonomodPatches).GetMethod(nameof(MonomodPatches.ActivateItem_performedPatch))));
            MMHooks.Add(new(typeof(PlayerControllerB).GetMethod(nameof(PlayerControllerB.Start), (BindingFlags)~0),
            typeof(MonomodPatches).GetMethod(nameof(MonomodPatches.PlayerStartPatch))));
        }
    }
    
    public enum Magicks
    {
        None,
        Fire,
        Gravity,
        Thunder,
        Blood
    }
}
