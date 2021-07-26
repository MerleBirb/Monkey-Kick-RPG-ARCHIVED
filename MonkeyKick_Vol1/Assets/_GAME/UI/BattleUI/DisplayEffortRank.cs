//===== DISPLAY EFFORT RANK =====//
/*
7/12/21
Description:
- Player's performance displayed after performing a button action

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.References;

namespace MonkeyKick.UI
{
    public class DisplayEffortRank : MonoBehaviour, IDisplayUI
    {
        #region PUBLIC FIELDS

        public StringReference EffortRankText;
        public Text EffortText;
        public RectTransform rectTransform;
        public float Time;

        public Color[] colors;

        #endregion

        #region PUBLIC METHODS

        public void DisplayUI()
        {
            EffortText.text = EffortRankText.Variable.Value;
            Destroy(gameObject, Time);
        }

        public void SetColor(object anInt)
        {
            EffortText.color = colors[(int)anInt];
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #endregion
    }
}
