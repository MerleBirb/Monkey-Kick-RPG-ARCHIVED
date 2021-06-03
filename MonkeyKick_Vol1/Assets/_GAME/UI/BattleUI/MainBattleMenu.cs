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
        private Image selector;
        private bool isCreated = false;
        [SerializeField] private List<Transform> menuTexts; // every text element in the menu
        [SerializeField] private int xOffset;

        private void Start()
        {
            isCreated = false;
        }

        private void Update()
        {
            if (!isCreated) { selector = Instantiate(selectorPrefab, transform); isCreated = true; }
            if (selector.rectTransform.rotation.z != 90f) { selector.rectTransform.rotation = Quaternion.Euler(0f, 0f, -90f); }

            MenuManager.SelectMenu(menuTexts, selector, menuChoice.Variable.Value, xOffset);

            if (menuChoice.Variable.Value < 0) { menuChoice.Variable.Value = menuTexts.Count - 1; }
            else if (menuChoice.Variable.Value > menuTexts.Count - 1) { menuChoice.Variable.Value = 0; }
        }
    }
}
