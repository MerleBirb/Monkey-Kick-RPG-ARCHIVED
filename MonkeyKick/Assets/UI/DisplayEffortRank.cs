// Merle Roji
// 10/23/21

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MonkeyKick.References;

namespace MonkeyKick
{
    public class DisplayEffortRank : MonoBehaviour, IDisplayUI
    {
        [SerializeField] private StringReference effortRankReference;
        [SerializeField] private TextMeshProUGUI effortRankText;
        [SerializeField] private float secondsUntilDestroyed;
        [SerializeField] private Color[] color;
        public RectTransform GetRectTransform() => GetComponent<RectTransform>();
        public void SetColorFromIndex(int index) => effortRankText.color = color[index];
        public void DisplayUI()
        {
            effortRankText.text = effortRankReference.Variable.Value;
            Destroy(gameObject, secondsUntilDestroyed);
        }
    }
}
