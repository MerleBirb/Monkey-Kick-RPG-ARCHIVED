using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// CHANGE SCENE ///
    /// Simple script that changes the scene
    /// 
    public string sn;
    
    // loads the next scene, self explanatory
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadNextScene(sn);
        }
    }
}
