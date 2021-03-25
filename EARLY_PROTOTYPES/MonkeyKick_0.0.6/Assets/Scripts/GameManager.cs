using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
    public static GameManager Instance;

    // the state the game is currently in
    public static GameStates GameState = GameStates.MAIN_MENU;

    // PlayerParty holds the stats of the player party.
    public List<Character> _playerParty = new List<Character>(4);
    public List<Character> PlayerParty
    {
        get
        {
            return _playerParty;
        }

        set
        {
            _playerParty = value;
        }
    }

    // the scenes
    [SerializeField]
    private string sceneName;
    public static Scene CurrentScene;
    private ChangeScene cs;

    // the controls for the main menu
    private PlayerInput controls;
    private bool pressedStart = false;

    /// FUNCTIONS ///

    /// Awake happens once the object activates
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        cs = GetComponent<ChangeScene>();
        controls = GetComponent<PlayerInput>();
        CurrentScene = SceneManager.GetActiveScene();
    }

    /// Update happens every single step
    private void Update()
    {
        UpdateState(GameState);
    }

    /// updates the state of the game
    private void UpdateState(GameStates state)
    {
        ChangeGameState();

        switch (state)
        {
            case GameStates.MAIN_MENU:
                {
                    MainMenu();

                    break;
                }
            case GameStates.OVERWORLD:
                {
                    Overworld();

                    break;
                }
            case GameStates.BATTLE:
                {
                    Battle();

                    break;
                }
        }
    }

    // main menu state
    private void MainMenu()
    {
        pressedStart = controls.actions.FindAction("Start").WasPressedThisFrame();

        if (pressedStart)
        {
            cs.LoadNextScene(sceneName);
            pressedStart = false;
        }
    }

    // overworld state
    private void Overworld()
    {

    }

    // battle state
    private void Battle()
    {

    }

    // changes scene depening on the suffix
    public void ChangeGameState()
    {
        if (SceneManager.GetActiveScene().name.Contains("OW"))
        {
            GameState = GameStates.OVERWORLD;
        }
        else if (SceneManager.GetActiveScene().name.Contains("BAT"))
        {
            GameState = GameStates.BATTLE;
        }
        else if (SceneManager.GetActiveScene().name.Contains("CUT"))
        {
            GameState = GameStates.CUTSCENE;
        }

        if (CurrentScene == SceneManager.GetActiveScene())
        {
            return;
        }

        CurrentScene = SceneManager.GetActiveScene();
    }
}