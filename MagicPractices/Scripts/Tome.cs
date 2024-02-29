using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace WizardTime.Scripts
{
    internal class Tome : NetworkBehaviour
    {
        public Spell? minorMagicks;
        public Spell? majorMagicks;
        public Spell? buffMagicks;
        public Spell? defensiveMagicks;
        public Spell? selectedSpell;
        public void CastSpellOnClient()
        {
            if (!IsOwner) return;
            CastSpellOnServerRpc();
        }
        [ServerRpc(RequireOwnership =false)]
        public void CastSpellOnServerRpc()
        {
            CastSpellOnClientRpc();
        }
        [ClientRpc]
        public void CastSpellOnClientRpc()
        {
            
            CastSpell();
        }
        public virtual void CastSpell()
        {
            if (selectedSpell == null) return;
            selectedSpell.SpellEffects();
            WizardTimePlugin.mls.LogInfo("click");
        }
    }
}
