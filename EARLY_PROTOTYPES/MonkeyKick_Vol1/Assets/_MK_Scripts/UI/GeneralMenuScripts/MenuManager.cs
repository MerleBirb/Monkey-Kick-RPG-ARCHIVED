//===== MENU MANAGER =====//
/*
6/2/21
Description:
- Handles logic that happens across many menus.

Author: Merlebirb
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick.UI
{
    public static class MenuManager
    {
        #region SELECT MENU FUNCTION

        public static void SelectMenu(List<Transform> _menu, Image _selector, int _menuChoice)
        {
            var _menuCount = _menu.Count - 1;
            bool _validChoice = _menuChoice >= 0 && _menuChoice <= _menuCount;

            if (_menu.Count > 0)
            {
                if (_validChoice)
                {
                    _selector.rectTransform.position = 
                    new Vector2(_menu[_menuChoice].position.x, _menu[_menuChoice].position.y);
                }
            }
        }

        public static void SelectMenu(List<Transform> _menu, Image _selector, int _menuChoice, int _xOffset)
        {
            var _menuCount = _menu.Count - 1;
            bool _validChoice = _menuChoice >= 0 && _menuChoice <= _menuCount;

            if (_menu.Count > 0)
            {
                if (_validChoice)
                {
                    _selector.rectTransform.position = 
                    new Vector2(_menu[_menuChoice].position.x + _xOffset, _menu[_menuChoice].position.y);
                }
            }
        }

        public static void SelectMenu(List<Transform> _menu, Image _selector, int _menuChoice, int _xOffset, int _yOffset)
        {
            var _menuCount = _menu.Count - 1;
            bool _validChoice = _menuChoice >= 0 && _menuChoice <= _menuCount;

            if (_menu.Count > 0)
            {
                if (_validChoice)
                {
                    _selector.rectTransform.position = 
                    new Vector2(_menu[_menuChoice].position.x + _xOffset, _menu[_menuChoice].position.y + _yOffset);
                }
            }
        }

        #endregion
    }
}
