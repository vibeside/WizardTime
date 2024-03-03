using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WizardTime.Scripts;
using WizardTime.SpellComponents;

namespace WizardTime
{
    internal static class AssetSummoner
    {
        public static List<Tome> tomes = [];
        public static List<Spell> spells = new();
        public static void SummonAssets()
        {
            foreach(Tome tome in WizardTimePlugin.magicksAssets.LoadAllAssets<Tome>())
            {
                tomes.Add(tome);
                WizardTimePlugin.mls.LogInfo(tome.MagicksType.ToString());
            }
            foreach(Spell spell in WizardTimePlugin.magicksAssets.LoadAllAssets<Spell>())
            {
                spells.Add(spell);
                WizardTimePlugin.mls.LogInfo(spell.Name.ToString());
            }
        }
    }
}
