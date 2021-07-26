//===== ACTION BUTTON ANIMATION =====//
/*
7/10/21
Description:
- The logic behind the action button's animation

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick.UI
{
    public class DisplayActionButton : MonoBehaviour, IDisplayUI
    {
    	#region PUBLIC FIELDS

        public Image image;
        public ActionButton button;
        public RectTransform rectTransform;

        #endregion

    	#region UNITY METHODS

        private void OnValidate()
        {
            DisplayUI();
        }

        #endregion

        #region PUBLIC METHODS

        public void DisplayUI()
        {
            if(image && button.buttonImage) image.sprite = button.buttonImage;
        }

        public void SetColor(object param1)
        {
            // empty
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #endregion
    }
}
