// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using MonkeyKick.Characters;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.UserInterface
{
    public class DisplayCharacterBattleUI : MonoBehaviour
    {
        [SerializeField] private TurnManager _turnManager;
        [SerializeField] private List<TextMeshProUGUI> _playerKiTexts;
        [SerializeField] private List<TextMeshProUGUI> _enemyKiTexts;
        private List<CharacterInformation> _playerStats = new List<CharacterInformation>();
        private List<CharacterInformation> _enemyStats = new List<CharacterInformation>();
        private bool _charactersLoaded = false;

        private void Awake()
        {
            InitUI();
        }

        private void OnDisable()
        {
            _playerStats.Clear();
            _enemyStats.Clear();
            _charactersLoaded = false;
        }

        private void LateUpdate()
        {
            DisplayUI();
        }

        public void InitUI()
        {
            for (int i = 0; i < _turnManager.TurnOrder.Count; ++i)
            {
                if (_turnManager.TurnOrder[i].Character.CompareTag(TagsQoL.PLAYER_TAG)) _playerStats.Add(_turnManager.TurnOrder[i].Character.Stats);
                else if (_turnManager.TurnOrder[i].Character.CompareTag(TagsQoL.ENEMY_TAG)) _enemyStats.Add(_turnManager.TurnOrder[i].Character.Stats);

                if (_turnManager.TurnOrder.Count - 1 == i) _charactersLoaded = true;
            }
        }

        public void DisplayUI()
        {
            if (_charactersLoaded)
            {
                for (int p = 0; p < _playerStats.Count; ++p)
                {
                    _playerKiTexts[p].text = "KI: " + _playerStats[p].CurrentKi.ToString() + "/" + _playerStats[p].MaxKi.ToString();
                }

                for (int e = 0; e < _enemyStats.Count; ++e)
                {
                    _enemyKiTexts[e].text = "KI: " + _enemyStats[e].CurrentKi.ToString() + "/" + _enemyStats[e].MaxKi.ToString();
                }
            }
        }
    }
}
