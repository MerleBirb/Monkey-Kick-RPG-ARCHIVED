// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
{
    public static class AnimationQoL
    {
        public static void ChangeAnimation(Animator anim, string currentAnim, string newAnim)
        {
            // converts strings to hashes for faster comparison
            int currentHash = Animator.StringToHash(currentAnim);
            int newHash = Animator.StringToHash(newAnim);

            if (currentHash.Equals(newHash)) return; // 'Equals()' faster than '=='
            currentAnim = newAnim;

            anim.Play(currentAnim);
        }
    }
}
