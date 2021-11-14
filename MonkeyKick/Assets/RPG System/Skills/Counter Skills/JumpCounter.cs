// Merle Roji
// 11/13/21

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Jump Counter", menuName = "RPGSystem/Counter Skills/Jump Counter", order = 1)]
    public class JumpCounter : CounterSkill
    {
        [Header("Height for the jump.")]
        [SerializeField] private float jumpHeight = 1f;

        private PlayerBattle _player;
        private EnemyBattle _enemy;

        public override void Init(CharacterBattle newActor, CharacterBattle newTarget)
        {
            base.Init(newActor, newTarget);

            _player = actor.GetComponent<PlayerBattle>();
            _enemy = actor.GetComponent<EnemyBattle>();
        }

        public override void Execute()
        {
            // jump action
            if (_player.pressedJump && _player.CharacterPhysics.OnGround())
            {
                _player.CharacterPhysics.Jump(jumpHeight);
            }
        }
    }
}
