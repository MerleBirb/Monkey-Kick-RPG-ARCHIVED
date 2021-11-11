// Merle Roji
// 11/11/21

using MonkeyKick.RPGSystem;
using MonkeyKick.UserInterface;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class SpawnEffortRankText : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private DisplayEffortRank _prefab; // prefab for UI
        private AttackRating _rating; // rating for the current attack being made
        private float _time; // time it takes for the UI to destroy
        private bool _hasSpawned = false; // has the UI spawned yet?

        public SpawnEffortRankText(Skill skill, DisplayEffortRank prefab, AttackRating rating, float time)
        {
            _skill = skill;
            _prefab = prefab;
            _rating = rating;
            _time = time;
        }

        public override bool Execute()
        {
            if (!_hasSpawned)
            {   
                _skill.InstantiateEffortRank(_prefab, _rating, _time);
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
