//===== GAME =====//
/*
5/12/21
Description:
- The static script that controls all managers.
- http://stackoverflow.com/a/35891919/294884 for source of what this script is based off of.

*/

using UnityEngine;
using UnityEngine.SceneManagement;

static class Game
{
    public static GameManager gameManager;
    public static PartyManager partyManager;
    public static TurnSystem turnSystem;

    static Game()
    {
        GameObject game = SafeFind("_app");

        gameManager = (GameManager)SafeComponent(game, "GameManager");
        partyManager = (PartyManager)SafeComponent(game, "PartyManager");
        turnSystem = (TurnSystem)SafeComponent(game, "TurnSystem");
    }

    private static GameObject SafeFind(string obj)
    {
        GameObject gObject = GameObject.Find(obj);

        if (gObject == null)
        {
            PreloadError("GameObject " + obj + " not on '_preload'.");
        }

        return gObject;
    }

    private static Component SafeComponent(GameObject gObject, string comp)
    {
        Component component = gObject.GetComponent(comp);

        if (component == null)
        {
            PreloadError("Component " + comp + " not on '_preload'.");
        }

        return component;
    }

    private static void PreloadError(string error)
    {
        Debug.LogWarning(">>> WARNING: " + error);
        Debug.LogWarning(">>> You probably forgot to start from '_preload'.");
    }
}
