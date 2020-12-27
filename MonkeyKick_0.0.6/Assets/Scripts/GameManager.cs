using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    public static GameStates GameState = GameStates.MAIN_MENU;

    // the cameras used in game, the overworld camera and battle camera
    public Camera overworldCamera;
    public Camera battleCamera;

    // the scenes
    [SerializeField]
    private string sceneName;
    private Scene currentScene;
    private Scene lastScene;
    private ChangeScene cs;

    // the controls for the main menu
    private PlayerInput controls;
    private bool pressedStart = false;

    /// FUNCTIONS ///

    // Awake happens once the object activates
    private void Awake()
    {
        cs = GetComponent<ChangeScene>();
        controls = GetComponent<PlayerInput>();
        currentScene = SceneManager.GetActiveScene();

        if (instance != null)
        {
            Destroy(this.gameObject);
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
        if (overworldCamera.gameObject.activeSelf)
        {
            overworldCamera.gameObject.SetActive(false);
        }

        if (battleCamera.gameObject.activeSelf)
        {
            battleCamera.gameObject.SetActive(false);
        }

        pressedStart = controls.actions.FindAction("Start").WasPressedThisFrame();

        if (pressedStart)
        {
            cs.LoadNextScene(sceneName);
            pressedStart = false;
        }

        if (SceneManager.GetActiveScene().name.Contains(sceneName))
        {
            GameState = GameStates.OVERWORLD;
        }
    }

    // overworld state
    private void Overworld()
    {
        if (!overworldCamera.gameObject.activeSelf)
        {
            overworldCamera.gameObject.SetActive(true);
        }

        if (battleCamera.gameObject.activeSelf)
        {
            battleCamera.gameObject.SetActive(false);
        }
    }

    // battle state
    private void Battle()
    {
        if (overworldCamera.gameObject.activeSelf)
        {
            overworldCamera.gameObject.SetActive(false);
        }

        if (!battleCamera.gameObject.activeSelf)
        {
            battleCamera.gameObject.SetActive(true);
        }
    }
}
