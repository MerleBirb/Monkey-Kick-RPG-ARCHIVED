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
        private enum TypeOfTarget
        {
            Player,
            Enemy
        }

        [SerializeField] private TypeOfTarget _typeOfTarget;

        public int DamageValue;
        [HideInInspector] public CharacterBattle Target;

        private bool _hasHit = false;

        private void OnTriggerEnter(Collider col)
        {
            if (_typeOfTarget == TypeOfTarget.Player)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.PLAYER_TAG))
                {
                    Target.Stats.Damage(DamageValue);
                    _hasHit = true;
                }
            }
            else if (_typeOfTarget == TypeOfTarget.Enemy)
            {
                if (!_hasHit && col.CompareTag(TagsQoL.ENEMY_TAG))
                {
                    Target.Stats.Damage(DamageValue);
                    Target.IsInterrupted = true;
                    _hasHit = true;
                }
            }
        }
    }
}
