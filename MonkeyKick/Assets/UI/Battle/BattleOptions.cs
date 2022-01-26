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

        private void OnEnable()
        {
            _selector = Instantiate(selectorPrefab, transform);
            _selector.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        private void OnDisable()
        {
            Destroy(_selector);
        }

        private void Update()
        {
            ChoiceUpdate();
        }

        private void ChoiceUpdate()
        {
            MenuQoL.SelectMenu(menuTexts, _selector, menuChoice.Variable.Value, OffsetChoice.XAxis, xOffset);

            if (menuChoice.Variable.Value < 0) { menuChoice.Variable.Value = menuTexts.Count - 1; }
            else if (menuChoice.Variable.Value > menuTexts.Count - 1) { menuChoice.Variable.Value = 0; }
        }
    }
}
