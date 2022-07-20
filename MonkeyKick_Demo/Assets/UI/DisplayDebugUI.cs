// Merle Roji 7/19/22

using UnityEngine;
using TMPro;

namespace MonkeyKick.UserInterface
{
    /// <summary>
    /// Displays Debug information.
    /// 
    /// </summary>
    public class DisplayDebugUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _effortRankText;

        public void DisplayTimer(float value)
        {
            _effortRankText.text = value.ToString("F2") + " sec.";
        }
    }
}
