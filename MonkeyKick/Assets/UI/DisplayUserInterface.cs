// Merle Roji
// 10/31/21 (happy halloween)

using UnityEngine;
using UnityEngine.UI;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public abstract class DisplayUserInterface : MonoBehaviour
    {
        public virtual void DisplayUI() { return; }
        public virtual void DisplayUI(AttackRating attackRating) { return; }
    }
}
