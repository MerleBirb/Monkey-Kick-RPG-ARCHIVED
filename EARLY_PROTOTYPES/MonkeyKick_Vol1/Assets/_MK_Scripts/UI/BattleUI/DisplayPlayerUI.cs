//===== DISPLAY PLAYER UI =====//
/*
7/15/21
Description:
- Displays the player's level, hp, and ki.

Author: Merlebirb
*/

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MonkeyKick.Battle;
using MonkeyKick.EntityInformation;

namespace MonkeyKick.UI
{
    public class DisplayPlayerUI : MonoBehaviour
    {
    	//===== VARIABLES =====//

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI kiText;

        [SerializeField] private CharacterPartyData playerParty;
        private List<CharacterInformation> _playerParty;

    	//===== INIT =====//

    	//===== METHODS =====//

        private void Start()
        {
            _playerParty = playerParty.CharacterList;
        }

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            for (int i = 0; i < _playerParty.Count; i++)
            {
                if (levelText.text != _playerParty[i].Level.ToString()) levelText.text = _playerParty[i].Level.ToString();
                if (hpText.text != _playerParty[i].CurrentHP.ToString()) hpText.text = _playerParty[i].CurrentHP.Stat.BaseValue.ToString();
                if (kiText.text != _playerParty[i].CurrentKP.ToString()) kiText.text = _playerParty[i].CurrentKP.Stat.BaseValue.ToString();
            }
        }
    }
}
