// Merle Roji
// 10/31/21 (spooky)

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem.Hitboxes
{
    public class Hitbox : MonoBehaviour
    {
        [HideInInspector] public int damage;
        [HideInInspector] public CharacterBattle target;

        private bool _hasHit = false;

        private void OnTriggerEnter(Collider col)
        {
            if (!_hasHit && col.CompareTag(TagsQoL.PLAYER_TAG))
            {
                target.Stats.Damage(damage);
                _hasHit = true;
            }
        }
    }
}
