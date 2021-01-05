using UnityEngine;
using CatlikeCoding.Movement;

public class ManagePlayerComponents : MonoBehaviour
{
    /// MANAGE PLAYER COMPONENTS ///
    /// This script is vital to the player; depending on the Game State, the player's components will turn on or off.

    /// VARIABLES ///

    // all the player components, their variable names are abbreviations
    private PlayerMovement playerMovement;
    private PlayerOverworldAnimations playerOverworldAnimations;

    // game state variables
    private GameStates currentGameState;
    private GameStates lastGameState;

    /// FUNCTIONS ///

    /// Awake is called the instant the player is awake
    private void Awake()
    {
        lastGameState = GameManager.GameState;

        playerMovement = GetComponent<PlayerMovement>();
        playerOverworldAnimations = GetComponent<PlayerOverworldAnimations>();
    }

    /// Update is called once per frame
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
                        playerMovement.CheckInput();

                        playerOverworldAnimations.UpdateFace();
                        playerOverworldAnimations.UpdateSounds();
                        playerOverworldAnimations.CleanSounds();

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

    /// FixedUpdate calls every frame in a fixed rate
    private void FixedUpdate()
    {
        if (GameManager.GameState == GameStates.OVERWORLD)
        {
            playerMovement.UpdateState();
            playerMovement.CheckMovement();
            playerMovement.ClearState();
        }
    }

    /// updates the saved state to the current one, returns a boolean
    private bool ChangedState()
    {
        if (lastGameState != currentGameState)
        {
            return true;
        }

        return false;
    }

    /// the overworld state of the player
    private void OverworldState()
    {
        if (playerMovement.enabled == false)
        {
            playerMovement.enabled = true;
        }

        if (playerOverworldAnimations.enabled == false)
        {
            playerOverworldAnimations.enabled = true;
        }
    }

    /// the battle state of the player
    private void BattleState()
    {
        if (playerMovement.enabled == true)
        {
            playerMovement.enabled = false;
        }

        if (playerOverworldAnimations.enabled == true)
        {
            playerOverworldAnimations.enabled = false;
        }
    }
}
