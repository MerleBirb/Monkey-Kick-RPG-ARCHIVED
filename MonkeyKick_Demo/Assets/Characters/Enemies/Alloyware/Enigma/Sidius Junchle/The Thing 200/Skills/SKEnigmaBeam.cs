// Merle Roji 8/4/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.Projectiles;
using MonkeyKick.UserInterface;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(fileName = "Enigma Beam", menuName = "RPG/Create a Skill/Alloyware/Enigma/Enigma Beam", order = 1)]
    public class SKEnigmaBeam : Skill
    {
        #region SKILL INFORMATION

        [SerializeField] protected DisplayDebugUI _debugInstructionsPrefab;

        [Header("Time it takes to charge the laser.")]
        [SerializeField] private float _timeToFire;
        [Header("Prefab of the laser beam.")]
        [SerializeField] private FireProjectile _laserPrefab;
        [Header("How fast the projectile should fly.")]
        [SerializeField] private float _projectileSpeed;
        [Header("How long until the projectile dies.")]
        [SerializeField] private float _projectileLifetime;
        [Header("A list of possible counters that the player can do")]
        [SerializeField] private Skill[] _possibleCounters;

        #endregion

        public override void Init(CharacterBattle newActor, CharacterBattle[] newTargets)
        {
            base.Init(newActor, newTargets);

            _allStates = new Dictionary<string, State>();
            Vector3 returnPos = new Vector3(Actor.BattlePos.x, Actor.transform.position.y, Actor.BattlePos.y);

            int damageScaling = (int)(Actor.Stats.Attack * _skillValue);

            SpriteRenderer renderer = Actor.GetComponentInChildren<SpriteRenderer>();

            State setUp = new State
            (
                // fixed update action
                null,
                // update action
                new StateAction[]
                {
                    new SADelayState(this, "prepareToFire", 0.1f),
                    new SASpawnDebugText(this, _debugInstructionsPrefab, "Press the 'Space' key to jump and avoid the laser. Hold the 'D' key to charge a punch, then release it to throw the punch.", 3f),
                    new SAInitCounterSkill(this, _possibleCounters),
                    new SASetPlayerStateFromEnemy(this, BattleStates.Counter)
                }
            );

            State prepareToFire = new State
            (
                // fixed update actions
                null,
                // update Actions
                new StateAction[]
                {
                    new SADelayState(this, "fireLaser", _timeToFire),
                    new SAChangeColorOverTime(renderer, Color.red, _timeToFire),
                    new SAExecuteCounterSkill(_possibleCounters, false)
                }
            );

            State fireLaser = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAChangeColor(renderer, Color.white),
                    new SAExecuteCounterSkill(_possibleCounters, false),
                    new SADelayState(this, "endSkill", _projectileLifetime - 0.1f),
                    new SASpawnProjectile(this, _laserPrefab, ActorTransform, _projectileSpeed, _projectileLifetime),
                    new SAInterruptAction(this, "interrupted"),
                }
            );

            State interrupted = new State
            (
                null,
                new StateAction[]
                {
                    new SADelayState(this, "endSkill", 0.1f), // the small time is a bandaid fix for weird freeze when hitting the enemy while theyre moving
                }
            );

            State endSkill = new State
            (
                // fixed update actions
                null,
                // update actions
                new StateAction[]
                {
                    new SAEndSkill(this),
                    new SASetPlayerStateFromEnemy(this, BattleStates.Wait),
                }
            );

            _allStates.Add("setUp", setUp);
            _allStates.Add("prepareToFire", prepareToFire);
            _allStates.Add("fireLaser", fireLaser);
            _allStates.Add("interrupted", interrupted);
            _allStates.Add("endSkill", endSkill);

            // set first state
            SetState("setUp");
        }
    }
}

