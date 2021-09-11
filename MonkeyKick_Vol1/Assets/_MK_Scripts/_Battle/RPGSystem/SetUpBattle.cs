//===== BATTLE PARTIES =====//
/*
5/23/21
Description:
- Holds the parties going into battle between scenes.

Author: Merlebirb
*/

using UnityEngine.SceneManagement;

namespace MonkeyKick.Battle
{
    public static class SetUpBattle
    {
        private static CharacterPartyData playerParty;
        private static CharacterPartyData enemyParty;

        public static CharacterPartyData GetPlayerParty() { return playerParty; }
        public static CharacterPartyData GetEnemyParty() { return enemyParty; }

        private static string PreviousScene;

        public static void SetPlayerParty(CharacterPartyData newParty)
        {
            if (playerParty == newParty) return;

            playerParty = null;
            playerParty = newParty;
        }

        public static void SetEnemyParty(CharacterPartyData newParty)
        {
            if (enemyParty == newParty) return;

            enemyParty = null;
            enemyParty = newParty;
        }

        public static void ClearPlayerParty()
        {
            playerParty.CharacterList.Clear();
            playerParty = null;
        }
        public static void ClearEnemyParty()
        {
            enemyParty.CharacterList.Clear();
            enemyParty = null;
        }

        public static void SavePreviousScene()
        {
            PreviousScene = SceneManager.GetActiveScene().name;
        }

        public static void LoadPreviousScene()
        {
            SceneManager.LoadScene(PreviousScene);
        }
    }
}