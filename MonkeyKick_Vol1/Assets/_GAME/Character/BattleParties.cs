//===== BATTLE PARTIES =====//
/*
5/23/21
Description:
- Holds the parties going into battle between scenes.

Author: Merlebirb
*/

using UnityEngine;
using MonkeyKick.Character;


namespace MonkeyKick.Character
{
    public static class BattleParties
    {
        private static CharacterPartyData PlayerParty;
        private static CharacterPartyData EnemyParty;

        public static CharacterPartyData GetPlayerParty() { return PlayerParty; }
        public static CharacterPartyData GetEnemyParty() { return EnemyParty; }

        public static void SetPlayerParty(CharacterPartyData _newParty)
        {
            if (PlayerParty == _newParty) return;

            PlayerParty = null;
            PlayerParty = _newParty;

            Debug.Log(">>> PLAYER PARTY COUNT: " + PlayerParty.CharacterList.Count);
        }

        public static void SetEnemyParty(CharacterPartyData _newParty)
        {
            if (EnemyParty == _newParty) return;

            EnemyParty = null;
            EnemyParty = _newParty;

            Debug.Log(">>> ENEMY PARTY COUNT: " + EnemyParty.CharacterList.Count);
        }

        public static void ClearPlayerParty() { PlayerParty = null; }
        public static void ClearEnemyParty() { EnemyParty = null; }
    }
}