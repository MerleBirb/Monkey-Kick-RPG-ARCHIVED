// Merle Roji
// 10/21/21

using UnityEngine;
using MonkeyKick.LogicPatterns.StateMachines;
using MonkeyKick.RPGSystem.Characters;
using MonkeyKick.RPGSystem.Hitboxes;
using MonkeyKick.QualityOfLife;
using MonkeyKick.UserInterface;

namespace MonkeyKick.RPGSystem
{
    public enum Buttons
    {
        North,
        East,
        South,
        West
    }

    public abstract class Skill : ScriptableObjectStateMachine
    {
        #region DESCRIPTORS

        [Header("Description for the Skill.")]
        [SerializeField] protected string skillName;
        [SerializeField] [TextArea(15, 20)] protected string skillDescription;

        [Header("Base Damage / Healing / Value for the skill to use")]
        [SerializeField] protected float skillValue;
        [HideInInspector] public AttackRating currentRating;

        #endregion

        #region CHARACTER FIELDS

        // actor
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

        #region STATE MACHINE METHODS

        // sets the actor, target, and actions of the attack
        public virtual void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            // set up actor
            actor = newActor;
            actorRb = actor.GetComponent<Rigidbody>();
            actorTransform = actor.transform;
            actorAnim = actor.GetComponentInChildren<Animator>();

            // set up target
            target = newTargets[0];
            targetRb = target.GetComponent<Rigidbody>();
            targetTransform = target.transform;
            targetAnim = target.GetComponentInChildren<Animator>();
        }

        #endregion

        #region INSTANTIATION METHODS

        // Hitbox
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

        // UI
        public virtual DisplayEffortRank InstantiateEffortRank(DisplayEffortRank prefab, AttackRating attackRating, float time)
        {
            DisplayEffortRank newRank = Instantiate(prefab);
            newRank.DisplayUI(attackRating);
            Destroy(newRank.gameObject, time);

            return newRank;
        }

        public virtual DisplayDebugUI InstantiateDebugUI(DisplayDebugUI prefab, float time)
        {
            DisplayDebugUI newDebugUI = Instantiate(prefab);
            Destroy(newDebugUI.gameObject, time);

            return newDebugUI;
        }

        #endregion
    }
}
