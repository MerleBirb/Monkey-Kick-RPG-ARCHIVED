// Merle Roji
// 10/13/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class PlayerBattle : CharacterBattle
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
