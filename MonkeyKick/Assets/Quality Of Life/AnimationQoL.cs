// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
{
    public static class AnimationQoL
    {
        public static void ChangeAnimation(in Animator anim, in string newAnim)
        {
            int currentAnimHash = anim.GetCurrentAnimatorStateInfo(0).GetHashCode(); // store the current animation's hashcode
            int newAnimHash = Animator.StringToHash(newAnim);

            if (currentAnimHash == newAnimHash) return; // return and don't change the animation if the current animation is equal to the new animation
            else if (currentAnimHash != newAnimHash) anim.Play(newAnimHash); // return and change the animation if the current animation is NOT equal to the new animation
        }

        public static void ChangeAnimation(in Animator anim, string currentAnim, in string newAnim)
        {
            // converts strings to hashes for faster comparison
            int currentHash = Animator.StringToHash(currentAnim);
            int newHash = Animator.StringToHash(newAnim);

            if (currentHash == newHash) return;
            currentAnim = newAnim;
            currentHash = newHash;

            anim.Play(currentHash);
        }

        public static void ChangeAnimation(in Animator anim, string currentAnim, in string newAnim, in bool flip)
        {
            // if flip, flip the sprite horizontally
            if (flip) anim.GetComponent<SpriteRenderer>().flipX = flip;

            // converts strings to hashes for faster comparison
            int currentHash = Animator.StringToHash(currentAnim);
            int newHash = Animator.StringToHash(newAnim);

            if (currentHash == newHash) return;
            currentAnim = newAnim;
            currentHash = newHash;

            anim.Play(currentHash);
        }
    }
}
