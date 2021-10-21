// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
{
    public static class AnimationQoL
    {
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
