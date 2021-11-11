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
        [SerializeField] private float xOffsetFromTarget;
        [SerializeField] private Hitbox hitboxPrefab;

        const string BATTLE_STANCE = "BattleStance_left";
        const string WINDUP = "Punch_windup_left";
        const string ATTACK = "Punch_attack_left";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            allStates = new Dictionary<string, State>();

            // set up actor
            actor = newActor;
            actorRb = actor.GetComponent<Rigidbody>();
            actorTransform = actor.transform;
            actorAnim = actor.GetComponentInChildren<Animator>();
            Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

            // set up target
            target = newTargets[0];
            targetRb = target.GetComponent<Rigidbody>();
            targetTransform = target.transform;
            targetAnim = target.GetComponentInChildren<Animator>();

            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            Vector3 hitboxScale = new Vector3(0.5f, 0.4f, 0.5f);

            State moveToPlayer = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new ActorMoveToTarget(this, "windUp", targetTransform.position, new Vector3(xVelToTarget, 0f, 0f), xOffsetFromTarget)
                },
                // update Actions
                new StateAction[]
                {
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
                    new DelayState(this, "launchAttack", 0.4f),
                    new SetPlayerToStateFromEnemy(this, BattleStates.Counter),
                    new ChangeAnimation(actorAnim, WINDUP)
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
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
                    new SetPlayerToStateFromEnemy(this, BattleStates.Wait),
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

            allStates.Add("moveToPlayer", moveToPlayer);
            allStates.Add("windUp", windUp);
            allStates.Add("launchAttack", launchAttack);
            allStates.Add("returnToBattlePos", returnToBattlePos);
            allStates.Add("endSkill", endSkill);

            // set first state
            SetState("moveToPlayer");
        }
    }
}
