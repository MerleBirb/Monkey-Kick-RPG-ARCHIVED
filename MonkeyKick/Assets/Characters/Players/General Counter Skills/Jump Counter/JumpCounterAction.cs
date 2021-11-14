// Merle Roji
// 11/14/21

using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class JumpCounterAction : StateAction
    {
        private PlayerBattle _player; // player that will be jumping
        private float _jumpHeight = 0f; // height for jump

        public JumpCounterAction(PlayerBattle player, float jumpHeight)
        {
            _player = player;
            _jumpHeight = jumpHeight;
        }

        public override bool Execute()
        {
            // jump action
            if (_player.pressedJump && _player.CharacterPhysics.OnGround())
            {
                _player.CharacterPhysics.Jump(_jumpHeight);
            }

            return false;
        }
    }
}
