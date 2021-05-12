//===== PLAYER CONTROLLER =====//
/*
5/11/21
Description: 
- Controls all the player logic. 
- Requires PlayerMovement and PlayerBattle components.

*/

using UnityEngine;

namespace Merlebirb.Characters
{
    public enum PlayerStates
    {
        OVERWORLD = 0,
        BATTLE = 1
    }

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerBattle))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerStates playerState; // determines what actions the player will be able to do

        private PlayerMovement playerMovement; // movement logic
        private PlayerBattle playerBattle; // battle logic

        // Start is called before the first frame update
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerBattle = GetComponent<PlayerBattle>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void ToggleState()
        {
            
        }
    }
}