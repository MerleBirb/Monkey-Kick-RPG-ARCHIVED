// Merle Roji 7/12/22

using UnityEngine;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.Characters.Enemies
{
    /// <summary>
    /// Handles battle logic for enemies.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public class EnemyBattle : CharacterBattle
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            switch(_battleState)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait: Wait(); break;
            }
        }
    }
}