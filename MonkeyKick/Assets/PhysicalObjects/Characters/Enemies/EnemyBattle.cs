// Merle Roji
// 10/13/12

using UnityEngine;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class EnemyBattle : CharacterBattle
    {
        private void Update()
        {
            switch (_battleState)
            {
                case BattleState.EnterBattle:
                    {
                        EnterBattle();
                        break;
                    }
            }
        }
    }
}