// Merle Roji
// 10/12/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterBattle : MonoBehaviour
    {
        public GameManager GameManager;

        private CharacterMovement _movement;

        #region UNITY METHODS

        public virtual void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
        }

        public virtual void Update()
        {
            if (!GameManager.InBattle())
            {
                if (GameManager.InOverworld()) _movement.enabled = true;

                this.enabled = false;
            }
        }

        public virtual void OnEnable()
        {
            if (!GameManager.InBattle()) this.enabled = false;
        }

        #endregion
    }
}
