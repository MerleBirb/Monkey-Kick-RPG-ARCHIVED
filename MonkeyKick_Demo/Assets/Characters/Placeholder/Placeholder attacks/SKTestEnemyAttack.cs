// Merle Roji 8/1/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPG/Create a Skill/Placeholder/Test Enemy Attack", order = 1)]
    public class SKTestEnemyAttack : Skill
    {
        #region SKILL INFORMATION

        [Header("Time it takes to arrive at the target")]
        [SerializeField] private float _timeToTarget;
        [Header("Offset on the x-axis for reaching target destination")]
        [SerializeField] private float _xOffsetFromTarget;
        [Header("Prefab for spawning the punch hitbox")]
        [SerializeField] private Hitbox _hitboxPrefab;
        [Header("A list of possible counters that the player can do")]
        [SerializeField] private Skill[] _possibleCounters;

        const string BATTLE_STANCE = "Idle_down";
        const string WINDUP = "Punch_windup_left";
        const string ATTACK = "Punch_attack_left";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            _allStates = new Dictionary<string, State>();
            Vector3 returnPos = new Vector3(Actor.BattlePos.x, Actor.transform.position.y, Actor.BattlePos.y);

            int damageScaling = (int)(Actor.Stats.Attack * _skillValue);
            Vector3 hitboxScale = new Vector3(0.3f, 0.3f, 0.3f);

            State setUp = new State
            (
                // fixed update action
                null,
                // update action
                new StateAction[]
                {
                    new SADelayState(this, "moveToPlayer", 0.1f),
                    new SAInitCounterSkill(this, _possibleCounters),
                    new SASetPlayerStateFromEnemy(this, BattleStates.Counter),
                }
            );

            State moveToPlayer = new State
            (
                // fixed update actions
                new StateAction[]
                {
                    new SALinearMoveToTarget(this, "windUp", TargetTransform.position, _timeToTarget, _xOffsetFromTarget),
                },
                // update Actions
                new StateAction[]
                {
                    new SAExecuteCounterSkill(_possibleCounters, false),
                    new SAInterruptAction(this, "interrupted"),
                    new SAChangeAnimation(ActorAnim, BATTLE_STANCE)
                }
            );

            State windUp = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAExecuteCounterSkill(_possibleCounters, false),
                    new SADelayState(this, "launchAttack", 0.25f),
                    new SAInterruptAction(this, "interrupted"),
                    new SAChangeAnimation(ActorAnim, WINDUP),
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAExecuteCounterSkill(_possibleCounters, false),
                    new SADelayState(this, "returnToBattlePos", 0.3f),
                    new SAInterruptAction(this, "interrupted"),
                    new SASpawnHitboxAtPoint(this, _hitboxPrefab, Actor.HitboxSpawnPoints[(int)BodyParts.LeftArm], hitboxScale, damageScaling, 0.3f),
                    new SAChangeAnimation(ActorAnim, ATTACK)
                }
            );

            State interrupted = new State
            (
                null,
                new StateAction[]
                {
                    new SADelayState(this, "returnToBattlePos", 0.02f), // the small time is a bandaid fix for weird freeze when hitting the enemy while theyre moving
                    new SAChangeAnimation(ActorAnim, BATTLE_STANCE)
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
                    new SAExecuteCounterSkill(_possibleCounters, false),
                    new SAChangeAnimation(ActorAnim, BATTLE_STANCE)
                }
            );

            State endSkill = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAChangeAnimation(ActorAnim, BATTLE_STANCE),
                    new SAChangeAnimation(TargetAnim, "BattleStance_right"),
                    new SAEndSkill(this),
                    new SASetPlayerStateFromEnemy(this, BattleStates.Wait),
                }
            );

            _allStates.Add("setUp", setUp);
            _allStates.Add("moveToPlayer", moveToPlayer);
            _allStates.Add("windUp", windUp);
            _allStates.Add("launchAttack", launchAttack);
            _allStates.Add("interrupted", interrupted);
            _allStates.Add("returnToBattlePos", returnToBattlePos);
            _allStates.Add("endSkill", endSkill);

            // set first state
            SetState("setUp");
        }
    }
}
