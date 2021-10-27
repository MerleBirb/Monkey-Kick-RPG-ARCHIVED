// Merle Roji
// 10/23/21

using UnityEngine;
using TMPro;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public class DisplayEffortRank : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI effortRankText;
        [SerializeField] private float secondsUntilDestroyed;
        [SerializeField] private Color[] color;
        private string _effortRankString;
        public void DisplayUI(AttackRating attackRating)
        {
            effortRankText.color = color[(int)attackRating];
            effortRankText.text = SkillQoL.AttackRatingStrings[(int)attackRating];
            Destroy(gameObject, secondsUntilDestroyed);
        }
    }
}
