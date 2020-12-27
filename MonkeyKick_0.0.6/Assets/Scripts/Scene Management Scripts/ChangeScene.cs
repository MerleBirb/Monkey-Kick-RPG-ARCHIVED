using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// CHANGE SCENE ///
    /// Simple script that changes the scene
    
    // loads the next scene, self explanatory
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
