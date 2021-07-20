//===== DISPLAY EFFORT RANK =====//
/*
7/12/21
Description:
- Player variation of a Skill

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.References;

namespace MonkeyKick.Skills
{
    public class DisplayEffortRank : MonoBehaviour
    {
        public StringReference EffortRankText;
        public Text EffortText;
        public RectTransform rectTransform;
        public float Time;

        private void Update()
        {
            EffortText.text = EffortRankText.Variable.Value;

            Destroy(gameObject, Time);
        }
    }
}
