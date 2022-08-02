// Merle Roji 8/1/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.Characters.Players;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(fileName = "Riposte Counter", menuName = "RPG/Create a Counter/Placeholder/Riposte Counter", order = 2)]
    public class SKRiposteCounter : Skill
    {
        [Header("Limit the windup timer goes to")]
        [SerializeField] private float _limitWindupTime;
        [Header("How long the attack lasts")]
        [SerializeField] private float _attackDelay;
        [Header("Hitbox prefab for the riposte")]
        [SerializeField] private Hitbox _hitboxPrefab;

        [HideInInspector] public float CounterTimer = 0f;

        #region ANIMATIONS

        const string BATTLE_STANCE = "Idle_down";
        const string WINDUP = "Punch_windup_right";
        const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            _allStates = new Dictionary<string, State>();
            CounterTimer = 0f;

            PlayerBattle player = Actor.GetComponent<PlayerBattle>();
            int damageScaling = (int)(Actor.Stats.Attack * _skillValue);
            Vector3 hitboxScale = new Vector3(0.3f, 0.3f, 0.3f);

            State prepping = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SARiposteCounterInput(this, "launchAttack", player.ButtonEast, WINDUP, BATTLE_STANCE, _limitWindupTime)
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SADelayState(this, "endRiposte", _attackDelay),
                    new SASpawnHitboxAtPoint(this, _hitboxPrefab, Actor.HitboxSpawnPoints[(int)BodyParts.RightArm], hitboxScale, damageScaling, _attackDelay),
                    new SAChangeAnimation(ActorAnim, ATTACK)
                }
            );

            State endRiposte = new State
            (
                null,
                new StateAction[]
                {
                    new SARiposteEnd(this),
                    new SADelayState(this, "prepping", 0.05f),
                    new SAChangeAnimation(ActorAnim, BATTLE_STANCE)
                }
            );

            _allStates.Add("prepping", prepping);
            _allStates.Add("launchAttack", launchAttack);
            _allStates.Add("endRiposte", endRiposte);

            // first state
            SetState("prepping");
        }
    }
}
