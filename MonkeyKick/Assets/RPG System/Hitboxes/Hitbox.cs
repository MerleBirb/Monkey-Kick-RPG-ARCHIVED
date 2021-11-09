// Merle Roji
// 10/31/21 (spooky)

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem.Hitboxes
{
    public class Hitbox : MonoBehaviour
    {
        private enum TypeOfTarget
        {
            Player,
            Enemy
        }
        [SerializeField] private TypeOfTarget typeOfTarget;

        [HideInInspector] public int damage;
        [HideInInspector] public CharacterBattle target;

        private bool _hasHit = false;

        private void OnTriggerEnter(Collider col)
        {
            if (typeOfTarget == TypeOfTarget.Player)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.PLAYER_TAG))
                {
                    target.Stats.Damage(damage);
                    _hasHit = true;
                }
            }
            else if (typeOfTarget == TypeOfTarget.Enemy)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.ENEMY_TAG))
                {
                    target.Stats.Damage(damage);
                    _hasHit = true;
                }
            }
        }
    }
}
