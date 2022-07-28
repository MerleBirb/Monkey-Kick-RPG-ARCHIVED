// Merle Roji 7/28/22

using MonkeyKick.Characters.Players;

namespace MonkeyKick.Skills
{
    public class SAJumpCounter : StateAction
    {
        private PlayerBattle _player; // player that will be jumping
        private float _jumpHeight = 0f; // height for jump

        public SAJumpCounter(PlayerBattle player, float jumpHeight)
        {
            _player = player;
            _jumpHeight = jumpHeight;
        }

        public override bool Execute()
        {
            // jump action
            if (_player.PressedJump && _player.OnGround())
            {
                _player.Jump(_jumpHeight);
            }

            return false;
        }
    }
}
