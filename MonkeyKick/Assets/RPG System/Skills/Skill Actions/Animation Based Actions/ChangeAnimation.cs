// Merle Roji
// 11/9/21

using System;
using UnityEngine;
using MonkeyKick.RPGSystem;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick
{
    public class ChangeAnimation : StateAction
    {
        private Animator _anim; // the animator that will play the new animation
        private string _newAnim; // the new animation

        public ChangeAnimation(Animator anim, string newAnim)
        {
            _anim = anim;
            _newAnim = newAnim;
        }

        public override bool Execute()
        {
            int currentAnimHash = _anim.GetCurrentAnimatorStateInfo(0).GetHashCode(); // store the current animation's hashcode
            int newAnimHash = Animator.StringToHash(_newAnim);

            if (currentAnimHash == newAnimHash)
            {
                return true;
            }
            else if (currentAnimHash != newAnimHash)
            {
                _anim.Play(newAnimHash);
                return true;
            }

            return false;
        }
    }
}
