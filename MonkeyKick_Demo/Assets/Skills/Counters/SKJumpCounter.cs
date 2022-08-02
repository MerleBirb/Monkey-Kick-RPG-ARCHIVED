// Merle Roji 8/1/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.Characters.Players;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(fileName = "Jump Counter", menuName = "RPG/Create a Counter/Placeholder/Jump Counter", order = 1)]
    public class SKJumpCounter : Skill
    {
        [Header("Height for the jump.")]
        [SerializeField] private float _jumpHeight = 1f;

        #region ANIMATIONS

        protected const string BATTLE_STANCE = "Idle_down";
        protected const string WINDUP = "Punch_windup_right";
        protected const string ATTACK = "Punch_attack_right";

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            _allStates = new Dictionary<string, State>();

            PlayerBattle player = Actor.GetComponent<PlayerBattle>();

            State jump = new State
            (
                null,
                new StateAction[]
                {
                    new SAJumpCounter(player, _jumpHeight)
                }
            );

            _allStates.Add("jump", jump);
            SetState("jump");
        }
    }
}
