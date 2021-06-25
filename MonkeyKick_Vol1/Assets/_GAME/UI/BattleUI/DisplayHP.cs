//===== DISPLAY HP =====//
/*
6/2/21
Description:
- Display the health of the character.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.Battle;

namespace MonkeyKick.UI
{
    public class DisplayHP : MonoBehaviour
    {
        [SerializeField] private Text HPText;
        [SerializeField] private CharacterInformation info;

        // Update is called once per frame
        private void Update()
        {
            UpdateHPText();
        }

        private void UpdateHPText()
        {
            var _updateText = "HP: " + info.CurrentHP.Stat.BaseValue + " / " + info.MaxHP.Stat.BaseValue;
            if (HPText.text.Contains(_updateText)) { return; }

            HPText.text = _updateText;
        }
    }
}
