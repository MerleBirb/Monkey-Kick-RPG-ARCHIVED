// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Test Player Attack", menuName = "Skills/Player Skills/Placeholder/Test Attack", order = 1)]
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
            Vector3 returnPos = new Vector3(actorPos.x, actorPos.y, targetPos.z); // line up the battle pos

            // prepare the time it takes to move to the target as a WaitForSeconds
            WaitForSeconds time = new WaitForSeconds(timeItTakesToMoveToTarget -= Time.fixedDeltaTime);

            yield return null;

            // move to target
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, targetPos, timeItTakesToMoveToTarget, xOffsetFromTarget);
            yield return time;

            yield return new WaitForSeconds(1.0f); // wait 1 second

            // move back to original position
            actorRb.velocity = PhysicsQoL.LinearMove(actorPos, targetPos, timeItTakesToMoveToTarget, xOffsetFromTarget);

        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(CoroutineAction(actor, target));
        }
    }
}
