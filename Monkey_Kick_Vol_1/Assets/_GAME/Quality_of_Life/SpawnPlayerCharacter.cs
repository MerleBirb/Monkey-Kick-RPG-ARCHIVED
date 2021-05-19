//===== SPAWN PLAYER CHARACTER =====//
/*
5/19/21
Description:
- Spawns in whoever the current main character is.

*/

using UnityEngine;
using Cinemachine;

public class SpawnPlayerCharacter : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCam;

    private void Awake()
    {
        CheckIfMainPlayerExists();
    }

    private void CheckIfMainPlayerExists()
    {
        if (Game.partyManager.mainPlayer != null)
        {
            var player = Instantiate(Game.partyManager.mainPlayer, transform.position, 
                Game.partyManager.mainPlayer.transform.rotation);

            if (cinemachineCam == null)
            {
                Debug.LogError(">>>ERROR: Cinemachine camera doesn't exist! Make sure to set it in the inspector.");
            }
            else
            {
                cinemachineCam.Follow = player.transform;
            }
        }
    }
}
