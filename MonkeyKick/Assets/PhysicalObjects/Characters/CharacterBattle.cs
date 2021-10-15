// Merle Roji
// 10/12/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterBattle : MonoBehaviour
    {
        [SerializeField] protected GameManager gameManager;

        private ManageCharacterState _state;

        #region UNITY METHODS

        public virtual void Awake()
        {
            if (gameManager.GameState == GameStates.Battle)
            {
                
            }
            else this.enabled = false;
        }

        #endregion
    }
}
