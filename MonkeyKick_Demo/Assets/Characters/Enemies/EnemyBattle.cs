// Merle Roji 7/12/22

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

            switch (_battleState)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait:
                    {
                        CheckKi();
                        Wait();
                        
                        break;
                    }
                case BattleStates.ChooseAction: ChooseAction(); break;
                case BattleStates.Action:
                    {
                        CheckKi();
                        Stats.Skills[0].Tick();

                        break;
                    }
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            switch (_battleState)
            {
                case BattleStates.Action:
                    {
                        Stats.Skills[0].FixedTick();

                        break;
                    }
            }
        }

        protected void ChooseAction()
        {
            // save battle position for returning from skills and counterattacks
            _battlePos.x = transform.position.x;
            _battlePos.y = transform.position.z;

            Stats.Skills[0].Init(this, new CharacterBattle[] { _turnManager.PlayerParty[0] });
            _battleState = BattleStates.Action;
        }
    }
}