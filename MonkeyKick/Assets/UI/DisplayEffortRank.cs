// Merle Roji
// 10/23/21

using UnityEngine;
using TMPro;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public class DisplayEffortRank : DisplayUserInterface
    {
        [SerializeField] private TextMeshProUGUI effortRankText;
        [SerializeField] private float secondsUntilDestroyed;
        [SerializeField] private Color[] color;

        public override void DisplayUI(AttackRating attackRating)
        {
            effortRankText.color = color[(int)attackRating];
            effortRankText.text = SkillQoL.AttackRatingStrings[(int)attackRating];
            Destroy(gameObject, secondsUntilDestroyed);
        }
    }
}
