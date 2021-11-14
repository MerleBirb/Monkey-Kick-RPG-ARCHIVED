// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.UserInterface;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPGSystem/Skills/Player Skills/Placeholder/Test Attack", order = 1)]
    public class TestPlayerAttack : Skill
    {
        #region SKILL INFORMATION

        [Header("To display how good the player did with their button press")]
        [SerializeField] protected DisplayEffortRank effortRankPrefab;
        [SerializeField] protected DisplayDebugUI debugUIPrefab;

        [Header("Velocity on the x-axis while moving towards the target")]
        [SerializeField] private float xVelToTarget;
        [Header("Time you have to input your button press")]
        [SerializeField] private float timeForPunch;
        [Header("Offset on the x-axis for reaching target destination")]
        [SerializeField] private float xOffsetFromTarget;
        [Header("Your attack rating is proportional to what time interval you press your input in")]
        [SerializeField] private float[] timeChecks;

        const string BATTLE_STANCE = "BattleStance_right";
        const string WINDUP = "Punch_windup_right";
        const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            allStates = new Dictionary<string, State>();
            Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

            State moveToEnemy = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "timedInputAttack", targetTransform.position, new Vector3(xVelToTarget, 0f, 0f), xOffsetFromTarget)
                },
                // update Actions
                new StateAction[]
                {
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            State timedInputAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SingleTapTimedInput(this, "finishAttack", timeForPunch, timeChecks, effortRankPrefab),
                    new SpawnDebugUI(this, debugUIPrefab, timeForPunch),
                    new ChangeAnimation(actorAnim, WINDUP)
                }
            );

            State finishAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new DamageTarget(this, "returnToBattlePos", actor.Stats.Muscle, skillValue, 0.25f),
                    new ChangeAnimation(actorAnim, ATTACK)
                }
            );

            State returnToBattlePos = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "endSkill", returnPos, new Vector3(-xVelToTarget, 0f, 0f))
                },
                // update actions
                new StateAction[]
                {
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            State endSkill = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new EndSkill(this),
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            allStates.Add("moveToEnemy", moveToEnemy);
            allStates.Add("timedInputAttack", timedInputAttack);
            allStates.Add("finishAttack", finishAttack);
            allStates.Add("returnToBattlePos", returnToBattlePos);
            allStates.Add("endSkill", endSkill);

            // set first state
            SetState("moveToEnemy");
        }
    }
}
