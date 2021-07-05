// ===== SKILL =====//
/*
6/2/21
Description:
- Abstract class for skills used in battle.

Author: Merlebirb
*/

using MonkeyKick.Stats;
using UnityEngine;

namespace MonkeyKick.Battle
{
    public abstract class Skill : ScriptableObject
    {
        private Rigidbody _rb;
        protected Vector3 _defaultGravity;

        public string skillName;
        [Multiline] public string skillDescription;
        public int skillValue;

        public virtual void Action(CharacterBattle actor, CharacterBattle target)
        {
            return;
        }

        public string GetName()
        {
            return skillName;
        }

        public string GetDescription()
        {
            return skillDescription;
        }

        public virtual void Damage(CharacterBattle target)
        {
            bool damageGoesBelowZero = (target.Stats.CurrentHP.ConstantValue.Value - skillValue) <= 0;

            if (damageGoesBelowZero)
            {
                target.Stats.CurrentHP.SetStat(0);
                target.Kill();
            }
            else
            {
                target.Stats.CurrentHP.ChangeStat(-skillValue);
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
    }
}
