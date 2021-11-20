// Merle Roji
// 10/21/21

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using MonkeyKick.RPGSystem;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.UserInterface
{
    public class DisplayPlayerStats : DisplayUserInterface
    {
        #region STAT TEXTS

        [SerializeField] private TurnSystem turnSystem;
        [SerializeField] private List<TextMeshProUGUI> playerHPTexts;
        [SerializeField] private List<TextMeshProUGUI> playerKiTexts;
        [SerializeField] private List<TextMeshProUGUI> enemyHPTexts;
        [SerializeField] private List<TextMeshProUGUI> enemyKiTexts;
        private List<CharacterStats> _playerStats = new List<CharacterStats>();
        private List<CharacterStats> _enemyStats = new List<CharacterStats>();
        private bool _charactersLoaded = false;

        #endregion

        private void OnEnable()
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

        private void InitUI()
        {
            for(int i = 0; i < turnSystem.TurnOrder.Count; ++i)
            {
                if (turnSystem.TurnOrder[i].character.CompareTag(TagsQoL.PLAYER_TAG)) _playerStats.Add(turnSystem.TurnOrder[i].character.Stats);
                else if (turnSystem.TurnOrder[i].character.CompareTag(TagsQoL.ENEMY_TAG)) _enemyStats.Add(turnSystem.TurnOrder[i].character.Stats);

                if (turnSystem.TurnOrder.Count - 1 == i) _charactersLoaded = true;
            }
        }

        public override void DisplayUI()
        {
            if (_charactersLoaded)
            {
                for (int p = 0; p < _playerStats.Count; ++p)
                {
                    playerHPTexts[p].text = "HP: " + _playerStats[p].CurrentHP.ToString() + "/" + _playerStats[p].MaxHP.ToString();
                    playerKiTexts[p].text = "KI: " + _playerStats[p].CurrentKi.ToString() + "/" + _playerStats[p].MaxKi.ToString();
                }

                for (int e = 0; e < _enemyStats.Count; ++e)
                {
                    enemyHPTexts[e].text = "HP: " + _enemyStats[e].CurrentHP.ToString() + "/" + _enemyStats[e].MaxHP.ToString();
                    enemyKiTexts[e].text = "KI: " + _enemyStats[e].CurrentKi.ToString() + "/" + _enemyStats[e].MaxKi.ToString();
                }
            }
        }
    }
}
