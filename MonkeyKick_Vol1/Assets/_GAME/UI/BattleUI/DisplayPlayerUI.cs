//===== DISPLAY PLAYER UI =====//
/*
7/15/21
Description:
- Displays the player's level, hp, and ki.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MonkeyKick.Battle;

namespace MonkeyKick.UI
{
    public class DisplayPlayerUI : MonoBehaviour
    {
    	//===== VARIABLES =====//

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI kiText;

        [SerializeField] private CharacterInformation player;

    	//===== INIT =====//

    	//===== METHODS =====//

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (levelText.text != player.Level.ToString()) levelText.text = player.Level.ToString();
            if (hpText.text != player.CurrentHP.ToString()) hpText.text = player.CurrentHP.Stat.BaseValue.ToString();
            if (kiText.text != player.CurrentKP.ToString()) kiText.text = player.CurrentKP.Stat.BaseValue.ToString();
        }
    }
}
