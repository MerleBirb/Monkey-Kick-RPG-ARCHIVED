//===== LOAD SCENE ON AWAKE =====//
/*
5/13/21
Description:
- Loads a Scene when this object is awake.

*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAwake : MonoBehaviour
{
    public string sceneToLoad;

    private void Awake()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
