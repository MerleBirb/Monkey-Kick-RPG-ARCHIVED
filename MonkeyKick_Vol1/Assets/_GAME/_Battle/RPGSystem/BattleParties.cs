//===== BATTLE PARTIES =====//
/*
5/23/21
Description:
- Holds the parties going into battle between scenes.

Author: Merlebirb
*/

using UnityEngine;


namespace MonkeyKick.Battle
{
    public static class BattleParties
    {
        private static CharacterPartyData PlayerParty;
        private static CharacterPartyData EnemyParty;

        public static CharacterPartyData GetPlayerParty() { return PlayerParty; }
        public static CharacterPartyData GetEnemyParty() { return EnemyParty; }

        public static void SetPlayerParty(CharacterPartyData newParty)
        {
            if (PlayerParty == newParty) return;

            PlayerParty = null;
            PlayerParty = newParty;
        }

        public static void SetEnemyParty(CharacterPartyData newParty)
        {
            if (EnemyParty == newParty) return;

            EnemyParty = null;
            EnemyParty = newParty;
        }

        public static void ClearPlayerParty() { PlayerParty = null; }
        public static void ClearEnemyParty() { EnemyParty = null; }
    }
}