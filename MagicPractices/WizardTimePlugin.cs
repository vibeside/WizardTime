using BepInEx;
using BepInEx.Logging;
using MonoMod.RuntimeDetour;
using System;
using System.IO;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using WizardTime.Scripts;

namespace WizardTime
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WizardTimePlugin : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.WizardTime";
        public const string modName = "MagicPractices";
        public const string modVersion = "0.1.0.0";
        public static ManualLogSource? mls;
        public static GameObject? focusOrb;
        private (uint, uint, uint, uint) QuadHash(int SALT = 0)
        { // [!code ++]
            Hash128 longHash = new Hash128();
            longHash.Append(modGUID);
            longHash.Append(SALT);
            return ((uint)longHash.u64_0, (uint)(longHash.u64_0 >> 32),
                    (uint)longHash.u64_1, (uint)(longHash.u64_1 >> 32));
        }
        public void Awake()
        {
            mls = base.Logger;
            //Holds the scripts used by the mod to do magic!
            focusOrb = new GameObject("Magical Glowing Orb");
            focusOrb.AddComponent<NetworkObject>();
            var (hash, _, _, _) = QuadHash(0);
            focusOrb.GetComponent<NetworkObject>().GlobalObjectIdHash = hash;
            //new Hook(
            //typeof(GameNetworkManager).GetMethod("Start", (BindingFlags)int.MaxValue),
            //(Action<GameNetworkManager> original, GameNetworkManager self) =>
            //{
            //    original(self);
            //});
            
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
