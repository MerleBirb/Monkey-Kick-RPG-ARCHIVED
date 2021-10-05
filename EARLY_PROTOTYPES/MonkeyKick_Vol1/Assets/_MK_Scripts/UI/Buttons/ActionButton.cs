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

        //===== METHODS =====//

        #region OVERLOADS

        public static bool operator ==(ActionButton a, ActionButton b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }
        public static bool operator !=(ActionButton a, ActionButton b)
        {
            return !(a == b);
        }

        public bool Equals(ActionButton other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return buttonImage.Equals(other.buttonImage)
                && anim.Equals(other.anim);
        }

        public override bool Equals(object obj)
        {
            return Equals((ActionButton)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = buttonImage.GetHashCode();
                hashCode = (hashCode * 397) ^ anim.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
