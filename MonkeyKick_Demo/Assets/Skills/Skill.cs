// Merle Roji 7/19/22

using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.UserInterface;
using MonkeyKick.Projectiles;

namespace MonkeyKick.Skills
{
    public enum Buttons
    {
        North,
        East,
        South,
        West
    }

    /// <summary>
    /// The parent class of all skills in the game.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public abstract class Skill : ScriptableObjectStateMachine
    {
        [Header("Description for the Skill.")]
        [SerializeField] protected string _skillName;
        [SerializeField] [TextArea(15, 20)] protected string _skillDescription;

        [Header("Base Damage / Healing / Value for the skill to use")]
        [SerializeField] protected float _skillValue;
        [HideInInspector] public AttackRating CurrentRating;

        // actor
        [HideInInspector] public CharacterBattle Actor;
        [HideInInspector] public Transform ActorTransform;
        [HideInInspector] public Rigidbody ActorRb;
        [HideInInspector] public Animator ActorAnim;

        // target
        [HideInInspector] public CharacterBattle Target;
        [HideInInspector] public Transform TargetTransform;
        [HideInInspector] public Rigidbody TargetRb;
        [HideInInspector] public Animator TargetAnim;

        // sets the actor, target, and actions of the attack
        public virtual void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            // set up actor
            Actor = newActor;
            ActorRb = Actor.GetComponent<Rigidbody>();
            ActorTransform = Actor.transform;
            ActorAnim = Actor.GetComponentInChildren<Animator>();

            // set up target
            Target = newTargets[0];
            TargetRb = Target.GetComponent<Rigidbody>();
            TargetTransform = Target.transform;
            TargetAnim = Target.GetComponentInChildren<Animator>();
        }

        public virtual FireProjectile InstantiateProjectile(FireProjectile prefab, Transform spawnPoint, float xSpeed, float lifetime, CharacterBattle target)
        {
            FireProjectile newProjectile = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            newProjectile.XSpeed = xSpeed;
            newProjectile.ProjHitbox.Target = target;
            Destroy(newProjectile, lifetime);

            return newProjectile;
        }

        // Hitbox
        public virtual Hitbox InstantiateHitbox(Hitbox prefab, Transform bodyPart, Vector3 scale, CharacterBattle target, int damage, float time)
        {
            Hitbox newHitbox = Instantiate(prefab, bodyPart);
            newHitbox.transform.localScale = scale;
            newHitbox.transform.rotation = Quaternion.identity;
            newHitbox.Target = target;
            newHitbox.DamageValue = damage;
            Destroy(newHitbox.gameObject, time);

            return newHitbox;
        }

        // UI
        public virtual DisplayEffortRank InstantiateEffortRank(DisplayEffortRank prefab, AttackRating attackRating)
        {
            DisplayEffortRank newRank = Instantiate(prefab);
            newRank.DisplayUI(attackRating);

            return newRank;
        }

        public virtual DisplayDebugUI InstantiateDebugUI(DisplayDebugUI prefab, float time)
        {
            DisplayDebugUI newDebugUI = Instantiate(prefab);
            Destroy(newDebugUI.gameObject, time);

            return newDebugUI;
        }

    }
}
