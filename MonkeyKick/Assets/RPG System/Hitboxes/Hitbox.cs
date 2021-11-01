// Merle Roji
// 10/31/21 (spooky)

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem.Hitboxes
{
    public class Hitbox : MonoBehaviour
    {
        private int _damage;
        private CharacterBattle _target;

        public void Init(CharacterBattle target, int damage)
        {
            _target = target;
            _damage = damage;
        }

        public void DestroyAfterTime(float time)
        {
            Destroy(gameObject, time);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(TagsQoL.PLAYER_TAG))
            {
                _target.Stats.Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
