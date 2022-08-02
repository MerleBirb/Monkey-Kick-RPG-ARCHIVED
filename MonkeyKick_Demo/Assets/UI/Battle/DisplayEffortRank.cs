// Merle Roji 7/19/22

using UnityEngine;
using TMPro;

namespace MonkeyKick.UserInterface
{
    /// <summary>
    /// Displays the rank of the attack.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public class DisplayEffortRank : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _effortRankText;
        [SerializeField] private float _secondsUntilDestroyed;
        [SerializeField] private Color[] _colors;

        public void DisplayUI(AttackRating attackRating)
        {
            //_effortRankText.color = _colors[(int)attackRating];
            _effortRankText.text = SkillQoL.AttackRatingStrings[(int)attackRating];
            Destroy(gameObject, _secondsUntilDestroyed);
        }
    }
}
