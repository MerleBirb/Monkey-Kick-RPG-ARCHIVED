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
        public RectTransform GetRectTransform() => GetComponent<RectTransform>();

        #endregion

        private void LateUpdate()
        {
            DisplayUI();
        }

        public void DisplayUI()
        {
            hpText.text = "HP: " + character.CurrentHP.ToString() + "/" + character.MaxHP.ToString();
            kiText.text = "KI: " + character.CurrentKi.ToString() + "/" + character.MaxKi.ToString();
        }
    }
}
