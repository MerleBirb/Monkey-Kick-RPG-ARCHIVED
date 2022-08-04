// Merle Roji 7/28/22

using UnityEngine;

namespace MonkeyKick.Skills
{
    public class SAChangeAnimation : StateAction
    {
        private Animator _anim; // the animator that will play the new animation
        private string _newAnim; // the new animation

        public SAChangeAnimation(Animator anim, string newAnim)
        {
            _anim = anim;
            _newAnim = newAnim;
        }

        public override bool Execute()
        {
            AnimationQoL.ChangeAnimation(_anim, _newAnim);

            return false;
        }
    }
}

