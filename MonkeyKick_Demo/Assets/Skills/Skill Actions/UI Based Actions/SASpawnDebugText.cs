// Merle Roji 8/4/22

using UnityEngine;
using MonkeyKick.UserInterface;

namespace MonkeyKick.Skills
{
    public class SASpawnDebugText : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private DisplayDebugUI _prefab; // prefab for UI
        private string _text; // text
        private float _time; // time it takes for the UI to destroy
        private bool _hasSpawned = false; // has the UI spawned yet?
        private DisplayDebugUI _newDebugUI = null; // ui to be spawned

        public SASpawnDebugText(Skill skill, DisplayDebugUI prefab, string text, float time)
        {
            _skill = skill;
            _prefab = prefab;
            _text = text;
            _time = time;
        }

        public override bool Execute()
        {
            if (!_hasSpawned)
            {
                _newDebugUI = _skill.InstantiateDebugUI(_prefab, _time, _text);
                _hasSpawned = true;
            }

            return false;
        }
    }
}

