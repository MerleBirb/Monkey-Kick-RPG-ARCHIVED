//===== CHARACTER BATTLE =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the battle game state.

*/

using UnityEngine;

public enum BattleStates
{
    EnterBattle,
    Wait, 
    Active,
    Targeting,
    Action, 
    Return, 
    Counter,
    Reset

}

public abstract class CharacterBattle : MonoBehaviour
{
    [SerializeField] private GameStateData Game;

    public CharacterInformation stats;
    [ReadOnly] public TurnClass turnClass;

    protected BattleStates state;

    private void Start()
    {
        state = BattleStates.EnterBattle;
    }

    public virtual void Update()
    {
        if (!Game.CompareGameState(GameStates.Battle)) { this.enabled = false; }
    }
}
