using UnityEngine;
using CatlikeCoding.Movement;

public class ManagePlayerComponents : MonoBehaviour
{
    /// MANAGE PLAYER COMPONENTS ///
    /// This script is vital to the player; depending on the Game State, the player's components will turn on or off.

    /// VARIABLES ///

    // all the player components, their variable names are abbreviations
    private PlayerMovement PM;
    private PlayerOverworldAnimations POWA;

    // game state variables
    private GameStates currentGameState;
    private GameStates lastGameState;

    /// FUNCTIONS ///

    // Awake is called the instant the player is awake
    private void Awake()
    {
        lastGameState = GameManager.GameState;

        PM = GetComponent<PlayerMovement>();
        POWA = GetComponent<PlayerOverworldAnimations>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentGameState != GameManager.GameState)
        {
            currentGameState = GameManager.GameState;
        }

        if (ChangedState())
        {
            switch(GameManager.GameState)
            {
                case GameStates.OVERWORLD:
                    {
                        OverworldState();

                        break;
                    }
                case GameStates.BATTLE:
                    {
                        BattleState();

                        break;
                    }
            }
        }
    }

    // updates the saved state to the current one, returns a boolean
    private bool ChangedState()
    {
        if (lastGameState != currentGameState)
        {
            return true;
        }

        return false;
    }

    // the overworld state of the player
    private void OverworldState()
    {
        if (PM.enabled == false)
        {
            PM.enabled = true;
        }

        if (POWA.enabled == false)
        {
            POWA.enabled = true;
        }
    }

    // the battle state of the player
    private void BattleState()
    {
        if (PM.enabled == true)
        {
            PM.enabled = false;
        }

        if (POWA.enabled == true)
        {
            POWA.enabled = false;
        }
    }
}
