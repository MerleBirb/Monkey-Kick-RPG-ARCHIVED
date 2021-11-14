// Merle Roji
// 11/13/21

using UnityEngine;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.RPGSystem
{
    public enum CounterSkillList
    {
        Jump = 0,
        Riposte
    }

    public abstract class CounterSkill : ScriptableObject
    {
        #region DESCRIPTORS

        [Header("Description for the Counter.")]
        [SerializeField] protected string counterName;
        [SerializeField] [TextArea(15, 20)] protected string counterDescription;

        [Header("Base Damage / Value for the Counter to use")]
        [SerializeField] protected float counterValue;

        #endregion

        #region CHARACTER FIELDS

        [HideInInspector] public CharacterBattle actor;
        [HideInInspector] public Transform actorTransform;
        [HideInInspector] public Rigidbody actorRb;
        [HideInInspector] public Animator actorAnim;

        // target
        [HideInInspector] public CharacterBattle target;
        [HideInInspector] public Transform targetTransform;
        [HideInInspector] public Rigidbody targetRb;
        [HideInInspector] public Animator targetAnim;

        #endregion

        #region METHODS

        public virtual void Init(CharacterBattle newActor, CharacterBattle newTarget)
        {
            // set up actor
            actor = newActor;
            actorRb = actor.GetComponent<Rigidbody>();
            actorTransform = actor.transform;
            actorAnim = actor.GetComponentInChildren<Animator>();

            // set up target
            target = newTarget;
            targetRb = target.GetComponent<Rigidbody>();
            targetTransform = target.transform;
            targetAnim = target.GetComponentInChildren<Animator>();
        }

        public abstract void Execute();

        #endregion

        #region INSTANTIATE METHODS

        public virtual Hitbox InstantiateHitbox(Hitbox prefab, Transform bodyPart, Vector3 scale, CharacterBattle target, int damage, float time)
        {
            Hitbox newHitbox = Instantiate(prefab, bodyPart);
            newHitbox.transform.localScale = scale;
            newHitbox.transform.rotation = Quaternion.identity;
            newHitbox.target = target;
            newHitbox.damage = damage;
            Destroy(newHitbox.gameObject, time);

            return newHitbox;
        }

        #endregion
    }
}
