// Merle Roji
// 11/11/21

using UnityEngine;
using MonkeyKick.RPGSystem;
using MonkeyKick.UserInterface;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class SpawnDebugUI : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private DisplayDebugUI _prefab; // prefab for UI
        private float _time; // time it takes for the UI to destroy
        private bool _hasSpawned = false; // has the UI spawned yet?
        private DisplayDebugUI _newDebugUI = null; // ui to be spawned

        public SpawnDebugUI(Skill skill, DisplayDebugUI prefab, float time)
        {
            _skill = skill;
            _prefab = prefab;
            _time = time;
        }

        public override bool Execute()
        {
            if (!_hasSpawned)
            {   
                _newDebugUI = _skill.InstantiateDebugUI(_prefab, _time);
                _hasSpawned = true;
            }
            else
            {
                if (_time >= 0f)
                {
                    _time -= Time.deltaTime;
                    _newDebugUI.DisplayTimer(_time);
                }
                else
                {
                    return true;
                }
                
            }

            return false;
        }
    }
}
