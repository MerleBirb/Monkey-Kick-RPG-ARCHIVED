// Merle Roji 7/28/22

using UnityEngine;

namespace MonkeyKick.Skills
{
    public class SADamageTarget : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private int _totalDamage; // how much damage is dealt
        private float _currentTime; // the current time on the timer
        private bool _hasDamaged = false; // has the target been damaged

        public SADamageTarget(Skill skill, string targetState, int stat = 1, float skillValue = 1f, float delay = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _totalDamage = (int)(stat * skillValue);
            _currentTime = delay;
        }

        public override bool Execute()
        {
            if (_currentTime >= 0f)
            {
                _currentTime -= Time.deltaTime;

                if (!_hasDamaged)
                {
                    _skill.Target.Stats.Damage(_totalDamage);
                    _hasDamaged = true;
                }
            }
            else
            {
                _skill.SetState(_targetState);
                return true;
            }

            return false;
        }
    }
}

