// Merle Roji 7/20/22

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public class BattleOptions : MonoBehaviour
    {
        [SerializeField] private IntReference _menuChoice;
        [SerializeField] private List<Transform> _menuTexts;
        [SerializeField] private int _xOffset;
        [SerializeField] private Image _selectorPrefab;
        private Image _selector;

        private void OnEnable()
        {
            _selector = Instantiate(_selectorPrefab, transform);
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
            MenuQoL.SelectMenu(_menuTexts, _selector, _menuChoice.Variable.Value, OffsetChoice.XAxis, _xOffset);

            if (_menuChoice.Variable.Value < 0) { _menuChoice.Variable.Value = _menuTexts.Count - 1; }
            else if (_menuChoice.Variable.Value > _menuTexts.Count - 1) { _menuChoice.Variable.Value = 0; }
        }
    }
}
