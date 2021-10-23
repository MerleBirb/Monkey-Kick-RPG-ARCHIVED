// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Attack", menuName = "RPGSystem/Skills/Player Skills/Placeholder/Test Attack", order = 1)]
    public class TestPlayerAttack : Skill
    {
        [SerializeField] private float timeItTakesToMoveToTarget; // cannot make this more literal
        [SerializeField] private float xOffsetFromTarget;

        /// <summary>
        /// The actor is the one who uses the ability, while the target is the one who gets hit by the ability.
        /// </summary>
        private IEnumerator CoroutineAction(CharacterBattle actor, CharacterBattle target)
        {
            // save the rigidbody of the actor
            Rigidbody actorRb = actor.GetComponent<Rigidbody>();

            // save the positions of characters involved with the script
            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = new Vector3(target.transform.position.x + xOffsetFromTarget, 
                actor.transform.position.y, target.transform.position.z);
            Vector3 returnPos = new Vector3(actor.BattlePos.x, actor.transform.position.y, actor.BattlePos.y);

            yield return null;

            // move to target
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, targetPos, timeItTakesToMoveToTarget, xOffsetFromTarget);
            yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);
            actorRb.velocity = Vector3.zero; // stop movement when target is reached
            actorPos = actor.transform.position;

            // Damage the target
            int damageScaling = (int)(actor.Stats.Muscle * skillValue);
            target.Stats.Damage(damageScaling);
            yield return new WaitForSeconds(1.0f); // wait 1 second

            // move back to original position
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, returnPos, timeItTakesToMoveToTarget);
            yield return new WaitForSeconds(timeItTakesToMoveToTarget - Time.fixedDeltaTime);
            actorRb.velocity = Vector3.zero; // stop movement when original position is reached

            yield return null;
            actor.ResetAfterAction();
        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(CoroutineAction(actor, target));
        }
    }
}
