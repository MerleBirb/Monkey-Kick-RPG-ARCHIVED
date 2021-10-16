// Merle Roji
// 10/12/21

using System.Collections;
using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

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

        #region PHYSICS

        private IPhysics _physics;

        [Header("The position the character goes when entering combat")]
        [SerializeField] private Vector2 _startingBattlePos;
        protected Vector2 _battlePos;
        public Vector2 StartingBattlePos { get => _startingBattlePos; }

        #endregion

        protected BattleState _battleState = BattleState.EnterBattle;

        #region UNITY METHODS

        private void Awake()
        {
            _physics = GetComponent<CharacterPhysics>();
            _battleState = BattleState.EnterBattle;
            this.enabled = false;
        }

        private void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }

        #endregion

        #region BATTLE STATES

        protected virtual void EnterBattle()
        {
            // set up battle position
            StartCoroutine(JumpIntoBattlePosition());
            _battlePos.x = transform.position.x;
            _battlePos.y = transform.position.z;
            _battleState = BattleState.Wait;
        }

        private IEnumerator JumpIntoBattlePosition()
        {
            Vector3 landPos = transform.position + new Vector3(_startingBattlePos.x, 0f, _startingBattlePos.y);

            // jump into position
            ParabolaData jumpData = PhysicsQoL.CalculateParabolaData(transform.position, landPos, 1f, 0f, Physics.gravity.y);
            PhysicsQoL.ParabolaMove(jumpData, _physics?.GetRigidbody());
            yield return new WaitForSeconds(jumpData.TimeToTarget);

            // stop movement after jump
            _physics?.ResetMovement();
            yield return null;
        }

        #endregion
    }
}
