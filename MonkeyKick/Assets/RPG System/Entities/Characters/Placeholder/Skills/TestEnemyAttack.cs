// Merle Roji
// 10/26/21

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.RPGSystem.Characters;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPGSystem/Skills/Enemy Skills/Placeholder/Test Attack", order = 1)]
    public class TestEnemyAttack : Skill
    {
        #region SKILL INFORMATION

        [Header("Time it takes to arrive at the target")]
        [SerializeField] private float timeToTarget;
        [Header("Offset on the x-axis for reaching target destination")]
        [SerializeField] private float xOffsetFromTarget;
        [Header("Prefab for spawning the punch hitbox")]
        [SerializeField] private Hitbox hitboxPrefab;
        [Header("A list of possible counters that the player can do")]
        [SerializeField] private Skill[] possibleCounters;

        const string BATTLE_STANCE = "BattleStance_left";
        const string WINDUP = "Punch_windup_left";
        const string ATTACK = "Punch_attack_left";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            allStates = new Dictionary<string, State>();
            Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            Vector3 hitboxScale = new Vector3(0.3f, 0.3f, 0.3f);

            State setUp = new State
            (
                // fixed update action
                null,
                // update action
                new StateAction[]
                {
                    new DelayState(this, "moveToPlayer", 0.1f),
                    new InitCounterSkill(this, possibleCounters),
                    new SetPlayerToStateFromEnemy(this, BattleStates.Counter),
                }
            );

            State moveToPlayer = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "windUp", targetTransform.position, timeToTarget, xOffsetFromTarget),
                },
                // update Actions
                new StateAction[]
                {
                    new ExecuteCounterSkill(possibleCounters, false),
                    new InterruptState(this, "interrupted"),
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            State windUp = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new ExecuteCounterSkill(possibleCounters, false),
                    new DelayState(this, "launchAttack", 0.25f),
                    new InterruptState(this, "interrupted"),
                    new ChangeAnimation(actorAnim, WINDUP),
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new ExecuteCounterSkill(possibleCounters, false),
                    new DelayState(this, "returnToBattlePos", 0.3f),
                    new InterruptState(this, "interrupted"),
                    new InstantiateHitboxAtPoint(this, hitboxPrefab, actor.Hitboxes[(int)BodyParts.LeftArm], hitboxScale, damageScaling, 0.3f),
                    new ChangeAnimation(actorAnim, ATTACK)
                }
            );

            State interrupted = new State
            (
                null,
                new StateAction[]
                {
                    new DelayState(this, "returnToBattlePos", 0.02f), // the small time is a bandaid fix for weird freeze when hitting the enemy while theyre moving
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            State returnToBattlePos = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "endSkill", returnPos, timeToTarget)
                },
                // update actions
                new StateAction[]
                {
                    new ExecuteCounterSkill(possibleCounters, false),
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
                    new ChangeAnimation(actorAnim, BATTLE_STANCE),
                    new ChangeAnimation(targetAnim, "BattleStance_right"),
                    new EndSkill(this)
                }
            );

            allStates.Add("setUp", setUp);
            allStates.Add("moveToPlayer", moveToPlayer);
            allStates.Add("windUp", windUp);
            allStates.Add("launchAttack", launchAttack);
            allStates.Add("interrupted", interrupted);
            allStates.Add("returnToBattlePos", returnToBattlePos);
            allStates.Add("endSkill", endSkill);

            // set first state
            SetState("setUp");
        }
    }
}
