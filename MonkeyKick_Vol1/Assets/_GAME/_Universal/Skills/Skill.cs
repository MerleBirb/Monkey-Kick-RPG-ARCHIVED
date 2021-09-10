// ===== SKILL =====//
/*
6/2/21
Description:
- Abstract class for skills used in battle.

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.Battle;
using MonkeyKick.AudioFX;
using MonkeyKick.UI;

namespace MonkeyKick.Skills
{
    public abstract class Skill : ScriptableObject
    {
        //===== VARIABLES =====//
        private Rigidbody _rb;
        protected Vector3 _defaultGravity;
        protected Camera _mainCam;
        protected float _effortValueMultiplier;
        protected BattleSFX _battleSFX;
        [SerializeField] protected AudioTable AudioTable;
        [SerializeField] protected RectTransform effortRankPrefab;

        public string SkillName;
        [Multiline] public string SkillDescription;
        public int SkillValue;

        //===== METHODS =====//

        #region ACTION

        public virtual void Action(CharacterBattle actor, CharacterBattle target)
        {
            return;
        }
        //public virtual void Action(PlayerBattle actor, EnemyBattle target)
        //{
        //    return;
        //}
        //public virtual void Action(PlayerBattle actor, PlayerBattle target)
        //{
        //    return;
        //}
        //public virtual void Action(EnemyBattle actor, PlayerBattle target)
        //{
        //    return;
        //}
        //public virtual void Action(EnemyBattle actor, EnemyBattle target)
        //{
        //    return;
        //}

        #endregion

        public string GetName()
        {
            return SkillName;
        }

        public string GetDescription()
        {
            return SkillDescription;
        }

        public virtual void Damage(CharacterBattle target)
        {
            int finalValue = Mathf.Clamp(((int)((float)SkillValue * _effortValueMultiplier)), 1, 99999);
            target.Stats.CurrentHP.ChangeStat(-finalValue);

            bool damageGoesBelowZero = (target.Stats.CurrentHP.ConstantValue.BaseValue - SkillValue) <= 0;

            if (damageGoesBelowZero)
            {
                target.Stats.CurrentHP.SetStat(0);
                target.Kill();
            }
        }

        /// <summary>
        /// Turns off various variables from the rigidbody to make the physics entirely reliant on the skill.
        /// </summary>
        public virtual void FreeUpPhysics(CharacterBattle actor)
        {
            if (!_rb) _rb = actor.GetComponent<Rigidbody>();
            _rb.useGravity = false;

            _defaultGravity = Physics.gravity;
        }

        /// <summary>
        /// Resets all physics variables of the rigidbody
        /// </summary>
        public virtual void ResetPhysics(CharacterBattle actor)
        {
            if (!_rb) _rb = actor.GetComponent<Rigidbody>();
            _rb.useGravity = true;

            Physics.gravity = _defaultGravity;
        }

        #region CAMERA CONTROLS

        public virtual Vector3 GetCenterPoint(List<Transform> points, Vector3 offset)
        {
            if (points.Count == 1)
            {
                return points[0].position;
            }

            var bounds = new Bounds(points[0].position, Vector3.zero);
            for (int i = 0; i < points.Count; i++)
            {
                bounds.Encapsulate(points[i].position);
            }

            Vector3 centerPoint = bounds.center;
            Vector3 newPosition = centerPoint + offset;

            return newPosition;
        }

        #endregion

        #region PARABOLA

        /// <summary>
        /// Parabolas used for animations for skills
        /// </summary>
        public struct ParabolaData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public ParabolaData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }

        public virtual ParabolaData CalculateParabolaData(Vector3 firstPos, Vector3 secondPos, float jumpHeight, float targetHeight,
        float gravity)
        {
            Vector3 targetPos = new Vector3(secondPos.x, secondPos.y + targetHeight, secondPos.z);

            float displacementY = targetPos.y - firstPos.y;
            Vector3 displacementXZ = new Vector3 (targetPos.x - firstPos.x, 0, targetPos.z - firstPos.z);

            float time = Mathf.Sqrt((-2 * jumpHeight) / gravity) + Mathf.Sqrt(2 *(displacementY - jumpHeight) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * jumpHeight);
            Vector3 velocityXZ = displacementXZ / time;

            return new ParabolaData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }

        /// <summary>
        /// creates a jump using a parabola
        /// </summary>
        public virtual void ParabolaJump(float grav, ParabolaData parabola, CharacterBattle actor, CharacterBattle target)
        {
            Physics.gravity = Vector3.up * grav;

            actor.rb.velocity = parabola.initialVelocity;
            target.rb.mass = 10000;
        }

        #endregion
    
        #region LINEAR

        public virtual Vector3 LinearReturn(Vector3 battlePos, Vector3 currentPos, float time)
        {
            Vector3 returnPos = new Vector3(battlePos.x, currentPos.y, battlePos.z);
            return (returnPos - currentPos) / time;
        }

        #endregion

        #region UI

        protected virtual IDisplayUI InstantiateUI(RectTransform rectPrefab, Vector3 pos)
        {
            RectTransform canvas = Instantiate(rectPrefab, Vector3.zero, Quaternion.identity);
            IDisplayUI ui = canvas.GetComponentInChildren<IDisplayUI>();
            ui.DisplayUI();
            RectTransform uiTransform = ui.GetRectTransform();
            Vector2 viewportPos = _mainCam.WorldToViewportPoint(pos);
            Vector2 uiScreenPos = new Vector2(
            ((viewportPos.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

            Vector2 uiAnchor = uiTransform.anchoredPosition;
            float xPos = uiAnchor.x;
            float yPos = uiAnchor.y;
            xPos = Mathf.Clamp(xPos, uiScreenPos.x, Screen.width - uiTransform.sizeDelta.x);
            yPos = Mathf.Clamp(yPos, uiScreenPos.y, Screen.height - uiTransform.sizeDelta.y);
            uiAnchor.x = xPos;
            uiAnchor.y = yPos;
            uiTransform.anchoredPosition = uiAnchor;

            return ui;
        }

        #endregion
    }
}
