// Merle Roji
// 10/21/21

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.RPGSystem.Hitboxes;

namespace MonkeyKick.RPGSystem
{
    public abstract class Skill : ScriptableObject
    {
        #region DESCRIPTORS

        [Header("Description for the Skill.")]
        [SerializeField] protected string skillName;
        [SerializeField] [TextArea(15, 20)] protected string skillDescription;

        [Header("Base Damage / Healing / Value for the skill to use")]
        [SerializeField] protected float skillValue;

        #endregion

        // Action
        public abstract void Action(CharacterBattle actor, CharacterBattle target);

        #region HITBOX METHODS

        public virtual Hitbox InstantiateHitbox(Hitbox prefab, Transform bodyPart, Vector3 scale, CharacterBattle target, int damage, float time)
        {
            Hitbox newHitbox = Instantiate(prefab, bodyPart);
            newHitbox.transform.localScale = scale;
            newHitbox.target = target;
            newHitbox.damage = damage;
            Destroy(newHitbox.gameObject, time);

            return newHitbox;
        }

        #endregion
    }
}
