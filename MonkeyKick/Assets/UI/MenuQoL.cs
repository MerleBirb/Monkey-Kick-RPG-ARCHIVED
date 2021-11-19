// Merle Roji
// 10/21/21

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick.QualityOfLife
{
    public enum OffsetChoice
    {
        XAxis,
        YAxis,
    }

    public static class MenuQoL
    {
        /// <summary>
        /// Moves the Image to an element in the list of choices in the menu
        /// </summary>
        public static void SelectMenu(in List<Transform> menu, Image selector, in int menuChoice)
        {
            var menuCount = menu.Count - 1;
            bool validChoice = menuChoice >= 0 && menuChoice <= menuCount;

            if (menu.Count > 0 && validChoice)
            {
                selector.rectTransform.position = new Vector2(menu[menuChoice].position.x, menu[menuChoice].position.y);
            }
        }
        
        public static void SelectMenu(in List<Transform> menu, Image selector, in int menuChoice, OffsetChoice offsetChoice, int offset)
        {
            var menuCount = menu.Count - 1;
            bool validChoice = menuChoice >= 0 && menuChoice <= menuCount;

            if (menu.Count > 0 && validChoice)
            {
                switch(offsetChoice)
                {
                    case OffsetChoice.XAxis:
                    {
                        selector.rectTransform.position =
                            new Vector2(menu[menuChoice].position.x + offset, menu[menuChoice].position.y);
                        break;
                    }
                    case OffsetChoice.YAxis:
                    {
                        selector.rectTransform.position =
                            new Vector2(menu[menuChoice].position.x, menu[menuChoice].position.y + offset);
                        break;
                    }
                }
            }
        }

        public static void SelectMenu(in List<Transform> menu, Image selector, in int menuChoice, int xOffset, int yOffset)
        {
            var menuCount = menu.Count - 1;
            bool validChoice = menuChoice >= 0 && menuChoice <= menuCount;

            if (menu.Count > 0 && validChoice)
            {
                selector.rectTransform.position =
                    new Vector2(menu[menuChoice].position.x + xOffset, menu[menuChoice].position.y + yOffset);
            }
        }
    }
}
