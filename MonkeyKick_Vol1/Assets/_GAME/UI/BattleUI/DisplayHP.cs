//===== DISPLAY HP =====//
/*
6/2/21
Description:
- Display the health of the character.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.EntityInformation;

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
            var updateText = "HP: " + info.CurrentHP.Stat.BaseValue + " / " + info.MaxHP.Stat.BaseValue;
            if (HPText.text.Contains(updateText)) { return; }

            HPText.text = updateText;
        }
    }
}
