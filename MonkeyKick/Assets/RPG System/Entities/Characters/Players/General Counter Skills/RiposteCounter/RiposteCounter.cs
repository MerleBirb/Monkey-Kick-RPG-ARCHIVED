// Merle Roji
// 11/14/21

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.RPGSystem.Characters;
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

        [HideInInspector] public float counterTimer = 0f;

        #region ANIMATIONS

        const string BATTLE_STANCE = "BattleStance_right";
        const string WINDUP = "Punch_windup_right";
        const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            allStates = new Dictionary<string, State>();
            counterTimer = 0f;

            PlayerBattle player = actor.GetComponent<PlayerBattle>();
            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            Vector3 hitboxScale = new Vector3(0.3f, 0.3f, 0.3f);

            State prepping = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new RiposteCounterInput(this, "launchAttack", player.ButtonEast, WINDUP, BATTLE_STANCE, limitWindupTime)
                }
            );

            State launchAttack = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new DelayState(this, "endRiposte", attackDelay),
                    new InstantiateHitboxAtPoint(this, hitboxPrefab, actor.Hitboxes[(int)BodyParts.RightArm], hitboxScale, damageScaling, attackDelay),
                    new ChangeAnimation(actorAnim, ATTACK)
                }
            );

            State endRiposte = new State
            (
                null,
                new StateAction[]
                {
                    new RiposteCounterEnd(this),
                    new DelayState(this, "prepping", 0.05f),
                    new ChangeAnimation(actorAnim, BATTLE_STANCE)
                }
            );

            allStates.Add("prepping", prepping);
            allStates.Add("launchAttack", launchAttack);
            allStates.Add("endRiposte", endRiposte);

            // first state
            SetState("prepping");
        }
    }
}
