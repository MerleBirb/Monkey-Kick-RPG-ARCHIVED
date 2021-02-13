using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ////////// THE GAME MANAGER //////////
    private static GameManager instance;

    // boolean to activate dev tools
    public bool devMode;

    ////////// ENTERING AND EXITING COMBAT //////////
    // store the overworld and battle cameras
    public Camera overworldCamera;
    public Camera battleCamera;

    // store the overworld and battle UIs
    public GameObject overworldUI;
    public GameObject battleUI;

    // store the current scene
    private Scene currentScene;

    // is the party in combat or out? this boolean handles that
    public static bool inBattle = false;

    ////////// PLAYER PARTY //////////
    // store the characters currently in the player's party
    public static List<PlayerBattleScript> mainParty = new List<PlayerBattleScript>();

    // Awake is called before anything
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        DevTools();
        StartGame();

        if (currentScene.name != "Title_Screen")
        {
            ToggleOverworldBattleCameras();
            ToggleOverworldBattleUserInterfaces();

            if (mainParty.Count <= 0)
            {
                mainParty.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBattleScript>());
            }
        }
    }

    // dev tools
    private void DevTools()
    {
        if (devMode)
        {
            if (Input.GetButtonDown("Forcequit_dev"))
            {
                Application.Quit();
            }
        }
    }

    // switch cameras depending on the battle state
    private void ToggleOverworldBattleCameras()
    {
        if (!inBattle)
        {
            if (overworldCamera.gameObject.activeSelf != true)
            {
                overworldCamera.gameObject.SetActive(true);
            }

            if (battleCamera.gameObject.activeSelf != false)
            {
                battleCamera.GetComponent<TurnSystemScript>().enabled = false;
                battleCamera.gameObject.SetActive(false);
            }
        }
        else if (inBattle)
        {
            if (overworldCamera.gameObject.activeSelf != false)
            {
                overworldCamera.gameObject.SetActive(false);
            }

            if (battleCamera.gameObject.activeSelf != true)
            {
                battleCamera.GetComponent<TurnSystemScript>().enabled = true;
                battleCamera.gameObject.SetActive(true);
            }
        }
    }

    // toggle the UI's depending on the battle state
    private void ToggleOverworldBattleUserInterfaces()
    {
        if (!inBattle)
        {
            if (overworldUI.activeSelf != true)
            {
                overworldUI.SetActive(true);
            }

            if (battleUI.activeSelf != false)
            {
                battleUI.SetActive(false);
            }
        }
        else if (inBattle)
        {
            if (overworldUI.activeSelf != false)
            {
                overworldUI.SetActive(false);
            }

            if (battleUI.activeSelf != true)
            {
                battleUI.SetActive(true);
            }
        }
    }

    // starts the game... what else would it do?
    private void StartGame()
    {
        if (currentScene.name == "Title_Screen")
        {
            if (Input.GetButtonDown("START_Button"))
            {
                SceneManager.LoadScene("DemoLevel_v2");
                Destroy(gameObject);
            }
        }
    }
}
