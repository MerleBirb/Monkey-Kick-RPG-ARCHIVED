using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// ENUM ///
/// this enum covers the state the game can be in
public enum GameStates
{
    OVERWORLD = 100,
    BATTLE = 200,
    MAIN_MENU = 300,
    CUTSCENE = 400
}

public class GameManager : MonoBehaviour
{
    /// THE GAME MANAGER ///
    /// The Game Manager manages important parts of the game and is like the "glue" of the code. Many global variables and instances made here.

    /// VARIABLES ///

    // a singleton instance that makes only one instance of the game manager ever
    protected static GameManager instance;

    // the state the game is currently in
    public static GameStates GameState = GameStates.OVERWORLD;

    // the cameras used in game, the overworld camera and battle camera
    public Camera overworldCamera;
    public Camera battleCamera;

    // the scenes
    private Scene currentScene;
    private Scene lastScene;
    private bool changedScene;

    /// FUNCTIONS ///

    // Awake happens once the object activates
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update happens every single step
    private void Update()
    {
        UpdateState(GameState);
    }

    // updates the state of the game
    private void UpdateState(GameStates state)
    {
        switch(state)
        {
            case GameStates.OVERWORLD:
                {
                    if (!overworldCamera.gameObject.activeSelf)
                    {
                        overworldCamera.gameObject.SetActive(true);
                    }

                    if (battleCamera.gameObject.activeSelf)
                    {
                        battleCamera.gameObject.SetActive(false);
                    }

                    break;
                }
            case GameStates.BATTLE:
                {
                    if (overworldCamera.gameObject.activeSelf)
                    {
                        overworldCamera.gameObject.SetActive(false);
                    }

                    if (!battleCamera.gameObject.activeSelf)
                    {
                        battleCamera.gameObject.SetActive(true);
                    }

                    break;
                }
        }
    }
}
