// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.UserInterface
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        #region STAT TEXTS

        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI kiText;
        [SerializeField] private CharacterStats character;

        #endregion

        private void LateUpdate()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            hpText.text = "HP: " + character.CurrentHP.ToString() + "/" + character.MaxHP.ToString();
            kiText.text = "KI: " + character.CurrentKi.ToString() + "/" + character.MaxKi.ToString();
        }
    }
}
