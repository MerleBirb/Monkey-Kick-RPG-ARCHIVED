// Merle Roji 8/4/22

using UnityEngine;
using MonkeyKick.Projectiles;

namespace MonkeyKick.Skills
{
    public class SASpawnProjectile : StateAction
    {
        private Skill _skill;
        private FireProjectile _projectilePrefab; // prefab of the projectile
        private FireProjectile _newProjectile; // the spawned projectile
        private Transform _spawnPoint; // where the projectile spawns
        private float _xSpeed; // speed in the x axis
        private float _lifetime; // time before destroy

        public SASpawnProjectile(Skill skill, FireProjectile projectilePrefab, Transform spawnPoint, float xSpeed, float lifetime)
        {
            _skill = skill;
            _projectilePrefab = projectilePrefab;
            _newProjectile = null;
            _spawnPoint = spawnPoint;
            _xSpeed = xSpeed;
            _lifetime = lifetime;
        }

        public override bool Execute()
        {
            if (_newProjectile == null)
            {
                _newProjectile = _skill.InstantiateProjectile(_projectilePrefab, _skill.ActorTransform, _xSpeed, _lifetime);
                return true;
            }

            return false;
        }
    }
}
