// Merle Roji
// 11/14/21

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.Characters;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Riposte Counter", menuName = "RPGSystem/Counter Skills/Riposte Counter", order = 2)]
    public class RiposteCounter : Skill
    {
        [Header("Limit the windup timer goes to")]
        [SerializeField] private float limitWindupTime;
        [Header("How long the attack lasts")]
        [SerializeField] private float attackDelay;
        [Header("Hitbox prefab for the riposte")]
        [SerializeField] private Hitbox hitboxPrefab;

        #region ANIMATIONS

        protected const string BATTLE_STANCE = "BattleStance_right";
        protected const string WINDUP = "Punch_windup_right";
        protected const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            allStates = new Dictionary<string, LogicPatterns.StateMachines.State>();

            PlayerBattle player = actor.GetComponent<PlayerBattle>();
            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            Vector3 hitboxScale = new Vector3(0.3f, 0.3f, 0.3f);

            State idle = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new RiposteCounterPrepare(this, "prepping", player.ButtonEast),
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            State prepping = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new RiposteCounterInput(this, "prepared", "idle", player.ButtonEast, limitWindupTime),
                    new ChangeAnimation(actorAnim, WINDUP)
                }
            );

            State prepared = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new RiposteCounterLaunch(this, "launchAttack", player.ButtonEast),
                    new ChangeAnimation(actorAnim, WINDUP)
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                new StateAction[]
                {

                },
                // update actions
                new StateAction[]
                {
                    new DelayState(this, "idle", attackDelay),
                    new InstantiateHitboxAtPoint(this, hitboxPrefab, actor.HurtBoxes[(int)BodyParts.RightArm], hitboxScale, damageScaling, 0.1f),
                    new ChangeAnimation(actorAnim, ATTACK)
                }
            );

            allStates.Add("idle", idle);
            allStates.Add("prepping", prepping);
            allStates.Add("prepared", prepared);
            allStates.Add("launchAttack", launchAttack);

            // first state
            SetState("idle");
        }
    }
}
