// Merle Roji 7/19/22

using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick
{
    /// <summary>
    /// 
    /// </summary>
    public class Hitbox : MonoBehaviour
    {
        public enum TypeOfTarget
        {
            Player,
            Enemy
        }

        [SerializeField] private TypeOfTarget _typeOfTarget;

        public int DamageValue;

        private bool _hasHit = false;

        private void OnTriggerEnter(Collider col)
        {
            if (_typeOfTarget == TypeOfTarget.Player)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.PLAYER_TAG))
                {
                    col.GetComponent<CharacterBattle>().Stats.Damage(DamageValue);
                    _hasHit = true;
                }
            }
            else if (_typeOfTarget == TypeOfTarget.Enemy)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.ENEMY_TAG))
                {
                    col.GetComponent<CharacterBattle>().Stats.Damage(DamageValue);
                    col.GetComponent<CharacterBattle>().IsInterrupted = true;
                    _hasHit = true;
                }
            }
        }

        public void ToggleTarget(TypeOfTarget type)
        {
            _typeOfTarget = type;
        }
    }
}
