// Merle Roji
// 10/19/21

using UnityEngine;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.Managers;

namespace MonkeyKick.RPGSystem
{
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private List<CharacterBattle> _allCombatants = new List<CharacterBattle>();
        [SerializeField] private List<TurnClass> _turnOrder = new List<TurnClass>(); // all characters in battle
        public List<TurnClass> TurnOrder { get {return _turnOrder; } }

        #region UNITY METHODS

        private void Start()
        {
            FillTurnOrder();
            SortTurnOrder();
            ResetTurns();
        }

        void Update()
        {
            UpdateTurns();
        }

        #endregion

        #region METHODS

        private void FillTurnOrder()
        {
            // transfer the list to the turn order, clear from game manager when done
            _allCombatants.Clear();
            _allCombatants.AddRange(gameManager.CurrentFighters);
            gameManager.ClearCurrentFighters();

            for (int i = 0; i < _allCombatants.Count; i++)
            {
                _turnOrder.Add(_allCombatants[i].Turn);
                _turnOrder[i].character = _allCombatants[i];
                _turnOrder[i].speed = _allCombatants[i].Stats.Speed;
            }
        }

        private void SortTurnOrder()
        {
            _turnOrder.Sort((a, b) =>
            {
                var speedA = a.speed;
                var speedB = b.speed;

                // sort the speeds
                return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
            });
        }

        private void ResetTurns()
        {
            for (int i = 0; i < _turnOrder.Count; i++)
            {
                if (i == 0)
                {
                    _turnOrder[i].isTurn = true;
                    _turnOrder[i].wasTurnPrev = false;
                }
                else
                {
                    _turnOrder[i].isTurn = false;
                    _turnOrder[i].wasTurnPrev = false;
                }
            }
        }

        private void UpdateTurns()
        {
            for (int i = 0; i < _turnOrder.Count; i++)
            {
                if (!_turnOrder[i].wasTurnPrev)
                {
                    _turnOrder[i].isTurn = true;
                    break;
                }
                else if (i == _turnOrder.Count - 1 && _turnOrder[i].wasTurnPrev)
                {
                    ResetTurns();
                }
            }
        }

        #endregion
    }
}
