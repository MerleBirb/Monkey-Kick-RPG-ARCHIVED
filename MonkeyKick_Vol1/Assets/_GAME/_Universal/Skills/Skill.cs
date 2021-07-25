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
            bool damageGoesBelowZero = (target.Stats.CurrentHP.ConstantValue.BaseValue - SkillValue) <= 0;

            if (damageGoesBelowZero)
            {
                target.Stats.CurrentHP.SetStat(0);
                target.Kill();
            }
            else
            {
                int finalValue = Mathf.Clamp(((int)((float)SkillValue * _effortValueMultiplier)), 1, 99999);
                target.Stats.CurrentHP.ChangeStat(-finalValue);
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

        public virtual ParabolaData CalculateParabolaData(CharacterBattle actor, CharacterBattle target, float height, float gravity)
        {
            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y + target.Stats.Height, target.transform.position.z);

            float displacementY = targetPos.y - actorPos.y;
            Vector3 displacementXZ = new Vector3 (targetPos.x - actorPos.x, 0, targetPos.z - actorPos.z);

            float time = Mathf.Sqrt((-2 * height) / gravity) + Mathf.Sqrt(2 *(displacementY - height) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
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

        protected virtual RectTransform InstantiateUIPosition(RectTransform rectPrefab, Vector3 pos)
        {
            RectTransform canvas = Instantiate<RectTransform>(rectPrefab, Vector3.zero, Quaternion.identity);
            RectTransform ui = canvas.GetComponentInChildren<RectTransform>();
            Vector2 viewportPos = _mainCam.WorldToViewportPoint(pos);
            Vector2 uiScreenPos = new Vector2(
            ((viewportPos.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

            Vector2 uiAnchor = ui.anchoredPosition;
            float xPos = uiAnchor.x;
            float yPos = uiAnchor.y;
            xPos = Mathf.Clamp(xPos, uiScreenPos.x, Screen.width - ui.sizeDelta.x);
            yPos = Mathf.Clamp(yPos, uiScreenPos.y, Screen.height - ui.sizeDelta.y);
            uiAnchor.x = xPos;
            uiAnchor.y = yPos;
            ui.anchoredPosition = uiAnchor;

            return canvas;
        }

        #endregion
    }
}
