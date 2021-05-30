//===== PLAYER INFORMATION =====//
/*
5/22/21
Description:
- Holds playable character information.

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Character
{
    [CreateAssetMenu(fileName = "New Player", menuName = "New Character/New Player Character")]
    public class PlayerInformation : CharacterInformation
    {
        public int MaxEXP;
        public int CurrentEXP;
        public int TotalEXP;

        public override void OnValidate()
        {
            base.OnValidate();

            MaxEXP = Mathf.Clamp(MaxEXP, 1, statClamp);
            CurrentEXP = Mathf.Clamp(CurrentEXP, 0, MaxEXP);
        }
    }
}