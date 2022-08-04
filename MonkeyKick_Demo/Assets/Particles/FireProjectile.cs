// Merle Roji  8/4/22

using UnityEngine;
using MonkeyKick.Characters;

namespace MonkeyKick.Projectiles
{
    public class FireProjectile : MonoBehaviour
    {
        [SerializeField] private float _xSpeed = 1;
        public float XSpeed
        {
            get => _xSpeed;
            set => _xSpeed = value;
        }

        [HideInInspector] public CharacterBattle Target;

        private Hitbox _hitbox;
        public Hitbox ProjHitbox { get => _hitbox; }
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _hitbox = GetComponentInChildren<Hitbox>();
        }

        private void Update()
        {
            _rigidbody.velocity = new Vector3(_xSpeed, 0f, 0f);
        }
    }
}
