// Merle Roji 7/28/22

using UnityEngine;

namespace MonkeyKick.Skills
{
    public class SASpawnHitboxAtPoint : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private Hitbox _prefab; // store a prefab of the hitbox
        private Transform _spawnPoint; // the transform where the hitbox will be spawned from
        private Vector3 _scale = Vector3.one; // scaling for the hitbox
        private int _totalDamage; // damage that the hitbox will deal
        private float _time; // time until the hitbox is destroyed
        private Hitbox _newHitbox; // is the hitbox created

        public SASpawnHitboxAtPoint(Skill skill, Hitbox prefab, Transform spawnPoint, Vector3 scale, int totalDamage = 1, float time = 1f)
        {
            _skill = skill;
            _prefab = prefab;
            _spawnPoint = spawnPoint;
            _scale = scale;
            _totalDamage = totalDamage;
            _time = time;
        }

        public override bool Execute()
        {
            if (_newHitbox == null)
            {
                _newHitbox = _skill.InstantiateHitbox(_prefab, _spawnPoint, _scale, _skill.Target, _totalDamage, _time);
                return true;
            }

            return false;
        }
    }
}

