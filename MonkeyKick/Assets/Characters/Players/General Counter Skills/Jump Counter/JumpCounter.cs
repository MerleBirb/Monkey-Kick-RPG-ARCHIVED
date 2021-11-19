// Merle Roji
// 11/13/21

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.RPGSystem.Characters;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Jump Counter", menuName = "RPGSystem/Counter Skills/Jump Counter", order = 1)]
    public class JumpCounter : Skill
    {
        [Header("Height for the jump.")]
        [SerializeField] private float jumpHeight = 1f;

        #region ANIMATIONS

        protected const string BATTLE_STANCE = "BattleStance_right";
        protected const string WINDUP = "Punch_windup_right";
        protected const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            allStates = new Dictionary<string, State>();

            PlayerBattle player = actor.GetComponent<PlayerBattle>();

            State jump = new State
            (
                null,
                new StateAction[]
                {
                    new JumpCounterAction(player, jumpHeight)
                }
            );

            allStates.Add("jump", jump);
            SetState("jump");
        }
    }
}
