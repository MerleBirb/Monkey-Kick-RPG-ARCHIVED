//===== MAIN BATTLE MENU =====//
/*
6/2/21
Description:
- Logic for the battle menu

Author: Merlebirb
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.References;

namespace MonkeyKick.UI
{
    public class MainBattleMenu : MonoBehaviour
    {
        [SerializeField] private IntReference menuChoice;
        [SerializeField] private Image selectorPrefab;
        private Image _selector;
        private bool _isCreated = false;
        [SerializeField] private List<Transform> menuTexts; // every text element in the menu
        [SerializeField] private int xOffset;

        private void Start()
        {
            _isCreated = false;
        }

        private void Update()
        {
            if (!_isCreated) { _selector = Instantiate(selectorPrefab, transform); _isCreated = true; }
            if (_selector.rectTransform.rotation.z != 90f) { _selector.rectTransform.rotation = Quaternion.Euler(0f, 0f, -90f); }

            MenuManager.SelectMenu(menuTexts, _selector, menuChoice.Variable.Value, xOffset);

            if (menuChoice.Variable.Value < 0) { menuChoice.Variable.Value = menuTexts.Count - 1; }
            else if (menuChoice.Variable.Value > menuTexts.Count - 1) { menuChoice.Variable.Value = 0; }
        }
    }
}
