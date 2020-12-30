using UnityEngine;
using UnityEngine.SceneManagement;
using Kryz.CharacterStats;
using Merlebird.TurnBasedSystem;
using CatlikeCoding.Movement;

public class StartBattle : MonoBehaviour
{
    /// START BATTLE ///
    /// This script controls how the battle is started, what positions the players will go to, etc.

    /// VARIABLES ///
    [SerializeField]
    private string sceneName;

    /// FUNCTIONS /// 

    // Start is called before the first frame update
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called when the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains(sceneName))
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
    }
}
