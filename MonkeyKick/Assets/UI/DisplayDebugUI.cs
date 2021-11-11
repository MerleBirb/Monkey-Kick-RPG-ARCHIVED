// Merle Roji
// 10/26/21

using UnityEngine;
using TMPro;

namespace MonkeyKick.UserInterface
{
    public class DisplayDebugUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI effortRankText;

        public void DisplayTimer(float value)
        {
            effortRankText.text = value.ToString("F2") + " sec.";
        }
    }
}
