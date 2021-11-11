// Merle Roji
// 11/10/21

using System;
using UnityEngine;
using MonkeyKick.RPGSystem;
using MonkeyKick.RPGSystem.Hitboxes;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class InstantiateHitboxAtPoint : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private Hitbox _prefab; // store a prefab of the hitbox
        private Transform _spawnPoint; // the transform where the hitbox will be spawned from
        private Vector3 _scale = Vector3.one; // scaling for the hitbox
        private int _totalDamage; // damage that the hitbox will deal
        private float _time; // time until the hitbox is destroyed
        private bool _hasSpawned = false; // has the hitbox spawned yet?

        public InstantiateHitboxAtPoint(Skill skill, Hitbox prefab, Transform spawnPoint, Vector3 scale, int totalDamage = 1, float time = 1f)
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
            if (!_hasSpawned)
            {
                _skill.InstantiateHitbox(_prefab, _spawnPoint, _scale, _skill.target, _totalDamage, _time);
                _hasSpawned = true;
                return false;
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
