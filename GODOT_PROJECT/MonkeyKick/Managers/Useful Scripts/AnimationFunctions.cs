using Godot;
using System;

namespace Merlebirb
{
    //===== USEFUL ANIMATION FUNCTIONS =====/
    /*
    by: Merlebirb 3/24/2021

    Description: Generally useful functions for animations in the whole project.

    */

    public static class AnimationFunctions
    {
        public static void PlayAnim(AnimationPlayer animation, string name)
        {
            if (animation.CurrentAnimation == name)
            {
                return;
            }

            animation.Play(name);
        }
    }
}
