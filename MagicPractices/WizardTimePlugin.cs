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
        public const string modName = "Magic Practices";
        public const string modVersion = "0.1.0.0";
        public static List<Hook> MMHooks = [];
        public static AssetBundle? magicksAssets;
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
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
            // dont forgor to make a secret spell to cast "Into Cake"
            // vital
            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            magicksAssets = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "magicksbundle"));
            mls = Logger;
            mls.LogInfo("Loaded!");
            //Holds the scripts used by the mod to do magic!


            GameObject silly = new("container");
            silly.hideFlags = HideFlags.HideAndDontSave;
            silly.SetActive(false);


            focusOrb = new GameObject("Magical Glowing Orb");
            focusOrb.hideFlags = HideFlags.HideAndDontSave;
            focusOrb.transform.SetParent(silly.transform);
            focusOrb.AddComponent<SpellBook>();
            focusOrb.AddComponent<NetworkObject>();
            var (hash, _, _, _) = QuadHash(0);
                focusOrb.GetComponent<NetworkObject>().GlobalObjectIdHash = hash;


            MMHooks.Add(new(typeof(GameNetworkManager).GetMethod(nameof(GameNetworkManager.Start), (BindingFlags)~0),
                typeof(MonomodPatches).GetMethod(nameof(MonomodPatches.GameNetworkManagerPatch))));
            MMHooks.Add(new(typeof(StartOfRound).GetMethod(nameof(StartOfRound.Awake), (BindingFlags)~0),
                typeof(MonomodPatches).GetMethod(nameof(MonomodPatches.StartOfRoundAwake))));
            MMHooks.Add(new(typeof(PlayerControllerB).GetMethod(nameof(PlayerControllerB.ActivateItem_performed), (BindingFlags)~0),
            typeof(MonomodPatches).GetMethod(nameof(MonomodPatches.ActivateItem_performedPatch))));
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
