// Merle Roji
// 10/26/21

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;
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
                new StateAction[]
                {

                },
                // update actions
                new StateAction[]
                {
                    new SetPlayerToStateFromEnemy(this, BattleStates.Counter),
                    new ChangeAnimation(actorAnim, WINDUP)
                }
            );

            allStates.Add("moveToPlayer", moveToPlayer);
            allStates.Add("windUp", windUp);

            // set first state
            SetState("moveToPlayer");
        }

        /// <summary>
        /// The actor is the one who uses the ability, while the target is the one who gets hit by the ability.
        /// </summary>
        //private IEnumerator CoroutineAction(CharacterBattle actor, CharacterBattle target)
        //{
        //    // save the rigidbody of the actor
        //    Rigidbody actorRb = actor.GetComponent<Rigidbody>();

        //    // save the positions of characters involved with the script
        //    Vector3 actorPos = actor.transform.position;
        //    Vector3 targetPos = new Vector3(target.transform.position.x + xOffsetFromTarget, 
        //        actor.transform.position.y, target.transform.position.z);
        //    Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

        //    target.BattleState = BattleStates.Counter; // set the player to counter attack

        //    yield return null;

        //    // move to target
        //    actorRb.velocity = PhysicsQoL.LinearMove(actorPos, targetPos, timeItTakesToMoveToTarget, xOffsetFromTarget);
        //    yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);

        //    // wind up the attack
        //    actor.ChangeAnimation(WINDUP);
        //    actorRb.velocity = Vector3.zero; // stop movement when target is reached
        //    actorPos = actor.transform.position;
        //    yield return new WaitForSeconds(0.4f);

        //    // Damage the target with a hitbox
        //    actor.ChangeAnimation(ATTACK);
        //    int damageScaling = (int)(actor.Stats.Muscle * skillValue);
        //    Vector3 hitboxScale = new Vector3(0.5f, 0.4f, 0.5f);
        //    InstantiateHitbox(hitboxPrefab, actor.HurtBoxes[(int)BodyParts.LeftArm], hitboxScale, target, damageScaling, 0.10f);

        //    yield return new WaitForSeconds(0.3f);

        //    // move back to original position
        //    actor.ChangeAnimation(BATTLE_STANCE);
        //    actorRb.velocity = PhysicsQoL.LinearMove(actorPos, returnPos, timeItTakesToMoveToTarget);
        //    yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);

        //    actorRb.velocity = Vector3.zero; // stop movement when original position is reached
        //    yield return null;
        //    target.BattleState = BattleStates.Wait;
        //    actor.ResetAfterAction();
        //}
    }
}
