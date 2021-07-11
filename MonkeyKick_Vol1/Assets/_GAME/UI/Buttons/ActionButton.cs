//===== ACTION BUTTON =====//
/*
7/10/21
Description:
- Holds the information of an action button.

Author: Merlebirb
*/

using System;
using UnityEngine;

namespace MonkeyKick.UI
{
    [Serializable]
    public struct ActionButton
    {
    	//===== VARIABLES =====//
        public Sprite buttonImage; // what the button will be (for example, A, B, X, Y for xbox)
        public Animator anim;

    	//===== INIT =====//
        public ActionButton(Sprite buttonImage, Animator anim)
        {
            this.buttonImage = buttonImage;
            this.anim = anim;
        }
    }
}
