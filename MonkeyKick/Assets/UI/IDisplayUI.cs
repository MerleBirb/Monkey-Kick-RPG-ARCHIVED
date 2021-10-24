// Merle Roji
// 10/23/21

using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick
{
    public interface IDisplayUI
    {
        RectTransform GetRectTransform();
        void DisplayUI();
        void SetColorFromIndex(int index);
    }
}
