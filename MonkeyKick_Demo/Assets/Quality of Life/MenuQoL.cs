// Merle Roji 7/20/22

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MonkeyKick
{
    public enum OffsetChoice
    {
        XAxis,
        YAxis,
    }

    /// <summary>
    /// Custom Menu functions.
    /// 
    /// Notes:
    /// 
    /// </summary>
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
                switch (offsetChoice)
                {
                    case OffsetChoice.XAxis: // scroll thru x axis
                        {
                            selector.rectTransform.position =
                                new Vector2(menu[menuChoice].position.x + offset, menu[menuChoice].position.y);
                            break;
                        }
                    case OffsetChoice.YAxis: // scroll thru y axis
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

        /// <summary>
        /// Scrolls up or down through the menu.
        /// </summary>

        public static void ScrollThroughMenu(ref bool movePressed, ref int menuValue, Vector2 movement)
        {
            const float DEADZONE = 0.3f;

            // scrolling through the menu
            if (movement.y < -DEADZONE)
            {
                if (!movePressed)
                {
                    ++menuValue;
                    movePressed = true;
                }
            }
            else if (movement.y > DEADZONE)
            {
                if (!movePressed)
                {
                    menuValue--;
                    movePressed = true;
                }
            }
            else
            {
                movePressed = false;
            }
        }

        public delegate void OpenOverworldMenuTrigger();
        public static event OpenOverworldMenuTrigger OnOpenOverworldMenu;

        /// <summary>
        /// Invokes the 'OnOpenOverworldMenu' event.
        /// </summary>
        public static void InvokeOnOpenOverworldMenu()
        {
            OnOpenOverworldMenu?.Invoke();
        }

        public delegate void CloseOverworldMenuTrigger();
        public static event CloseOverworldMenuTrigger OnCloseOverworldMenu;

        /// <summary>
        /// Invokes the 'OnCloseOverworldMenu' event.
        /// </summary>
        public static void InvokeOnCloseOverworldMenu()
        {
            OnCloseOverworldMenu?.Invoke();
        }
    }
}