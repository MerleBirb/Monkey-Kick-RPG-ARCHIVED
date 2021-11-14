// Merle Roji
// 10/26/21

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPGSystem/Skills/Enemy Skills/Placeholder/Test Attack", order = 1)]
    public class TestEnemyAttack : Skill
    {
        #region SKILL INFORMATION

        [Header("Velocity on the x-axis while moving towards the target")]
        [SerializeField] private float xVelToTarget;
        [Header("Offset on the x-axis for reaching target destination")]
        [SerializeField] private float xOffsetFromTarget;
        [Header("Prefab for spawning the punch hitbox")]
        [SerializeField] private Hitbox hitboxPrefab;
        [Header("A list of possible counters that the player can do")]
        [SerializeField] private CounterSkill[] possibleCounters;

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
            Vector3 hitboxScale = new Vector3(0.5f, 0.4f, 0.5f);

            State setUp = new State
            (
                // fixed update action
                null,
                // update action
                new StateAction[]
                {
                    new DelayState(this, "moveToPlayer", 0.1f),
                    new SetPlayerToStateFromEnemy(this, BattleStates.Counter),
                    new InitCounterSkill(this, possibleCounters)
                }
            );

            State moveToPlayer = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "windUp", targetTransform.position, new Vector3(xVelToTarget, 0f, 0f), xOffsetFromTarget),
                },
                // update Actions
                new StateAction[]
                {
                    new ChangeAnimation(actorAnim, BATTLE_STANCE),
                    new ExecuteCounterSkill(this, possibleCounters)
                }
            );

            State windUp = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new ExecuteCounterSkill(this, possibleCounters),
                    new DelayState(this, "launchAttack", 0.4f),
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
                    new ExecuteCounterSkill(this, possibleCounters),
                    new DelayState(this, "returnToBattlePos", 0.4f),
                    new InstantiateHitboxAtPoint(this, hitboxPrefab, actor.HurtBoxes[(int)BodyParts.LeftArm], hitboxScale, damageScaling, 0.1f),
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
                    new SetPlayerToStateFromEnemy(this, BattleStates.Wait),
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

            allStates.Add("setUp", setUp);
            allStates.Add("moveToPlayer", moveToPlayer);
            allStates.Add("windUp", windUp);
            allStates.Add("launchAttack", launchAttack);
            allStates.Add("returnToBattlePos", returnToBattlePos);
            allStates.Add("endSkill", endSkill);

            // set first state
            SetState("setUp");
        }
    }
}
