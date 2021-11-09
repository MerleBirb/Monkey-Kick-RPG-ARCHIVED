// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;
using MonkeyKick.UserInterface;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPGSystem/Skills/Player Skills/Placeholder/Test Attack", order = 1)]
    public class TestPlayerAttack : Skill
    {
        #region SKILL INFORMATION

        [Header("To display how good the player did with their button press")]
        [SerializeField] protected DisplayUserInterface effortRankPrefab;
        [SerializeField] protected DisplayDebugUI debugUIPrefab;

        [Header("Specific Skill Variables.")]
        [SerializeField] private float timeItTakesToMoveToTarget;
        [SerializeField] private float timeForPunch;
        [SerializeField] private float xOffsetFromTarget;
        [SerializeField] private float[] timeChecks; 

        const string BATTLE_STANCE = "BattleStance_right";
        const string WINDUP = "Punch_windup_right";
        const string ATTACK = "Punch_attack_right";

        // actor
        protected PlayerBattle _actor;
        protected Transform _actorTransform;
        protected Rigidbody _actorRb;

        // target
        protected EnemyBattle _target;
        protected Transform _targetTransform;
        protected Rigidbody _targetRb;

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            // set up actor
            _actor = (PlayerBattle)newActor;
            _actorRb = _actor.GetComponent<Rigidbody>();
            _actorTransform = newActor.transform;
            
            // set up target
            _target = (EnemyBattle)newTargets[0];
            _targetRb = _target.GetComponent<Rigidbody>();
            _targetTransform = _target.transform;

            State moveToEnemy = new State
            (
                // Fixed Update actions
                new StateAction[]
                {

                },
                // Update actions
                new StateAction[]
                {

                }
            );
        }

        /// <summary>
        /// The actor is the one who uses the ability, while the target is the one who gets hit by the ability.
        /// </summary>
        private IEnumerator CoroutineAction(CharacterBattle actor, CharacterBattle target)
        {
            // save the rigidbody of the actor
            Rigidbody actorRb = actor.GetComponent<Rigidbody>();
            PlayerBattle actorPlayer = actor.GetComponent<PlayerBattle>();
            float currentTime = timeForPunch;
            AttackRating rating = AttackRating.Miss;

            // save the positions of characters involved with the script
            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = new Vector3(target.transform.position.x + xOffsetFromTarget, 
                actor.transform.position.y, target.transform.position.z);
            Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

            yield return null;

            // move to target
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, targetPos, timeItTakesToMoveToTarget, xOffsetFromTarget);
            yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);

            // wind up the attack
            actor.ChangeAnimation(WINDUP);
            actorRb.velocity = Vector3.zero; // stop movement when target is reached
            actorPos = actor.transform.position;

            DisplayDebugUI debugUI = Instantiate(debugUIPrefab);

            while(currentTime >= 0f)
            {
                currentTime -= Time.deltaTime;
                debugUI.DisplayUI(currentTime);

                if (actorPlayer.pressedButtonSouth)
                {
                    rating = SkillQoL.TimedButtonPress(currentTime, timeForPunch, timeChecks);
                    Destroy(debugUI.gameObject);
                    break;
                }

                yield return null;
            }

            Destroy(debugUI.gameObject);
            yield return null;

            // Damage the target
            actor.ChangeAnimation(ATTACK);

            // Display effort rank
            DisplayUserInterface effortRank = Instantiate(effortRankPrefab);
            effortRank.DisplayUI(rating);

            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            target.Stats.Damage(damageScaling);
            yield return new WaitForSeconds(0.25f);

            // move back to original position
            actor.ChangeAnimation(BATTLE_STANCE);
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, returnPos, timeItTakesToMoveToTarget);
            yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);

            actorRb.velocity = Vector3.zero; // stop movement when original position is reached
            yield return null;
            actor.ResetAfterAction();
        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(CoroutineAction(actor, target));
        }
    }
}
