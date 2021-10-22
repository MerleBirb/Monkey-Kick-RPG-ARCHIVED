// Merle Roji
// 10/21/21

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MonkeyKick.References;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public class BattleOptions : MonoBehaviour
    {
        [SerializeField] private IntReference menuChoice;
        [SerializeField] private List<Transform> menuTexts;
        [SerializeField] private int xOffset;
        [SerializeField] private Image selectorPrefab;
        private Image _selector;
        private bool _selectorCreated = false;

        private void Update()
        {
            ChoiceUpdate();
        }

        private void ChoiceUpdate()
        {
            if (!_selectorCreated)
            {
                _selector = Instantiate(selectorPrefab, transform);
                _selectorCreated = true;
            }

            MenuQoL.SelectMenu(menuTexts, _selector, menuChoice.Variable.Value, OffsetChoice.XAxis, xOffset);

            if (menuChoice.Variable.Value < 0) { menuChoice.Variable.Value = menuTexts.Count - 1; }
            else if (menuChoice.Variable.Value > menuTexts.Count - 1) { menuChoice.Variable.Value = 0; }
        }
    }
}
