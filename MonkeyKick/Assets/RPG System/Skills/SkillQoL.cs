// Merle Roji
// 10/26/21

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.QualityOfLife
{
    public enum AttackRating
    {
        Miss = 0,
        Ok = 1,
        Good = 2,
        Great = 3,
        Excellent = 4
    }

    public static class SkillQoL
    {
        public static readonly string[] AttackRatingStrings = { "MISS...", "OK!", "GOOD!", "GREAT!!", "EXCELLENT!!!" };

        public static AttackRating SingleTapTimedButtonPress(float currentTime, float limitTime, float[] timeChecks)
        {
            if (currentTime >= (limitTime * timeChecks[0])) { return AttackRating.Miss; }
            else if (currentTime >= (limitTime * timeChecks[1])) { return AttackRating.Ok; }
            else if (currentTime >= (limitTime * timeChecks[2])) { return AttackRating.Good; }
            else if (currentTime >= (limitTime * timeChecks[3])) {return AttackRating.Great; }
            else if (currentTime >= 0f) { return AttackRating.Excellent; }
            else { return AttackRating.Miss; }
        }

        public static bool HoldTimedButtonPress(float currentTime, float limitTime, InputAction button)
        {
            if (button.triggered)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= limitTime) return true;
            }

            return false;
        }
    }
}
