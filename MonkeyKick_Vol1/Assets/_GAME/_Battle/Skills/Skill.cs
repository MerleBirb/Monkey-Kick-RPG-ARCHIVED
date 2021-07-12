// ===== SKILL =====//
/*
6/2/21
Description:
- Abstract class for skills used in battle.

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;

namespace MonkeyKick.Battle
{
    public abstract class Skill : ScriptableObject
    {
        //===== VARIABLES =====//
        private Rigidbody _rb;
        protected Vector3 _defaultGravity;
        protected Camera _mainCam;

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
                target.Stats.CurrentHP.ChangeStat(-SkillValue);
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
            Vector3 targetPos = target.transform.position;

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
    }
}
