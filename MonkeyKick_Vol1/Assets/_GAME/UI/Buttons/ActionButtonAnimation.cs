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
    public class ActionButtonAnimation : MonoBehaviour
    {
    	//===== VARIABLES =====//
        public Image image;
        public ActionButton button;

    	//===== INIT =====//

        public void SetButton(ActionButton newButton)
        {
            if (button == newButton) return;
            button = newButton;
        }

    	//===== METHODS =====//

        private void OnValidate()
        {
            if(image && button.buttonImage) image.sprite = button.buttonImage;
        }
    }
}
