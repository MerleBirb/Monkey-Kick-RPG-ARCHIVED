// Merle Roji
// 10/12/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public enum BattleState
    {
        EnterBattle,
        Wait
    }

    public abstract class CharacterBattle : MonoBehaviour
    {
        [SerializeField] protected GameManager gameManager;

        [Header("The position the character goes when entering combat")]
        [SerializeField] private Vector2 _battlePos;
        public Vector2 BattlePos { get => _battlePos; }

        protected BattleState _battleState = BattleState.EnterBattle;

        #region UNITY METHODS

        private void Awake()
        {
            _battleState = BattleState.EnterBattle;
            this.enabled = false;
        }

        #endregion

        #region BATTLE STATES

        protected virtual void EnterBattle()
        {
            // set up battle position
            transform.position += new Vector3(BattlePos.x, 0f, BattlePos.y);
            _battleState = BattleState.Wait;
        }

        #endregion
    }
}
