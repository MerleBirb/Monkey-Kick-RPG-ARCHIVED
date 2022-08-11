// Merle Roji 7/28/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.UserInterface;
using MonkeyKick.Characters;
using MonkeyKick.Characters.Players;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPG/Create a Skill/Placeholder/Test Player Attack", order = 1)]
    public class SKTestPlayerAttack : Skill
    {
        [Header("To display how good the player did with their button press")]
        [SerializeField] protected DisplayEffortRank _effortRankPrefab;
        [SerializeField] protected DisplayDebugUI _debugTimerPrefab;
        [SerializeField] protected DisplayDebugUI _debugInstructionsPrefab;

        [Header("Time it takes to arrive at the target")]
        [SerializeField] private float _timeToTarget;
        [Header("Time you have to input your button press")]
        [SerializeField] private float _timeForPunch;
        [Header("Offset on the x-axis for reaching target destination")]
        [SerializeField] private float _xOffsetFromTarget;
        [Header("Your attack rating is proportional to what time interval you press your input in")]
        [SerializeField] private float[] _timeChecks;

        const string IDLE = "pdawg_battlestance";
        const string WINDUP = "pdawg_punch_windup";
        const string ATTACK = "pdawg_punch_hit";

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            _allStates = new Dictionary<string, State>();

            PlayerBattle player = Actor.GetComponent<PlayerBattle>();
            Vector3 returnPos = new Vector3(Actor.BattlePos.x, Actor.transform.position.y, Actor.BattlePos.y);

            State moveToEnemy = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new SALinearMoveToTarget(this, "timedInputAttack", TargetTransform.position, _timeToTarget, _xOffsetFromTarget)
                },
                // update Actions
                new StateAction[]
                {
                    new SASpawnDebugText(this, _debugInstructionsPrefab, "Press the 'D' key before the timer runs out.", 3f),
                    new SAChangeAnimation(ActorAnim, IDLE)
                }
            );

            State timedInputAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SASingleTapTimedInput(this, "finishAttack", player.ButtonEast, _timeForPunch, _timeChecks, _effortRankPrefab),
                    new SASpawnDebugTimer(this, _debugTimerPrefab, _timeForPunch),
                    new SAChangeAnimation(ActorAnim, WINDUP)
                }
            );

            State finishAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SADamageTarget(this, "returnToBattlePos", Actor.Stats.Attack, _skillValue, 0.25f),
                    new SAChangeAnimation(ActorAnim, ATTACK)
                }
            );

            State returnToBattlePos = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new SALinearMoveToTarget(this, "endSkill", returnPos, _timeToTarget)
                },
                // update actions
                new StateAction[]
                {
                    new SAChangeAnimation(ActorAnim, IDLE)
                }
            );

            State endSkill = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAEndSkill(this),
                    new SAChangeAnimation(ActorAnim, IDLE)
                }
            );

            _allStates.Add("moveToEnemy", moveToEnemy);
            _allStates.Add("timedInputAttack", timedInputAttack);
            _allStates.Add("finishAttack", finishAttack);
            _allStates.Add("returnToBattlePos", returnToBattlePos);
            _allStates.Add("endSkill", endSkill);

            // set first state
            SetState("moveToEnemy");
        }
    }
}