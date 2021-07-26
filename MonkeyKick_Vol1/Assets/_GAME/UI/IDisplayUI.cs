//===== INTERFACE DISPLAY UI =====//
/*
7/25/21
Description:
- Interface for UI Elements

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick.UI
{
    public interface IDisplayUI
    {
		RectTransform GetRectTransform();
		GameObject GetGameObject();
		void SetColor(object param1);
    	void DisplayUI();
    }
}
