//===== ANIMATION QUALITY OF LIFE =====//
/*
6/20/21
Description:
- Quality of life functions for animation

Author: Merlebirb
*/

using UnityEngine;
using System;

namespace MonkeyKick.QoL
{
    public static class AnimQoL
    {
        public static void PlayAnimation(Animator anim, string currentAnim, string newAnim)
        {
            if (currentAnim == newAnim) return;
            currentAnim = newAnim;

            anim.Play(currentAnim);
        }

        public static void TogglePause(Animator anim)
        {
            anim.enabled = !anim.enabled;
        }
    }
}
