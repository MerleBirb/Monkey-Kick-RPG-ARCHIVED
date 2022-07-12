// Merle Roji 7/11/22

using UnityEngine;
using UnityEngine.SceneManagement;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.Characters.Enemies
{
    /// <summary>
    /// Controls enemy movement during the overworld portions, and enters battle.
    /// 
    /// Notes:
    /// - make sure to add Game Manager
    /// - make sure to add Game State functionality
    /// </summary>
    public class EnemyOverworldPhysics : CharacterOverworldPhysics
    {

        [SerializeField] private string _sceneToLoad;
        [SerializeField] private PartyManager _party;
        public PartyManager Party { get => _party; }

        [Header("This is the party that will be saved and taken to battle. Do not change this.")]
        [SerializeField] private PartyManager _currentEnemyParty;

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(TagsQoL.PLAYER_TAG))
            {
                // set the new enemy party
                _currentEnemyParty.Characters = _party.Characters;

                // change the scene
                SceneManager.LoadScene(_sceneToLoad);
            }
        }
    }
}
