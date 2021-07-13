//===== DISPLAY SKILL RANKING =====//
/*
7/12/21
Description:
- Player variation of a Skill

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.References;

namespace MonkeyKick.UI
{
    public class DisplaySkillRank : MonoBehaviour
    {
        public StringReference EffortRankText;
        public Text text;

        private void Update()
        {
            text.text = EffortRankText.Variable.Value;
        }
    }
}
