using UnityEngine;

namespace Merlecode.Player
{
    /// ----- PLAYER MANAGER ----- ///
    /* Description:
     * 
     * Combines all the components of the player into one neat manager
     * 
     * - Merle, 2/13/2021
     * 
     * */

    public class PlayerManager : MonoBehaviour
    {
        #region COMPONENTS
        [SerializeField] private PlayerMovement playerMovement;
        private bool HasPlayerMovement => playerMovement != null; // checks if the component has been passed in

        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            if (HasPlayerMovement) { playerMovement.MovementStart(); }
        }

        // Awake is called when the object is activated
        private void Awake()
        {
            if (HasPlayerMovement) { playerMovement.MovementAwake(); }
        }

        // Update is called once per frame
        private void Update()
        {
            if (HasPlayerMovement) { playerMovement.MovementUpdate(); }
        }

        // FixedUpdate is called once per frame at fixed framerate
        private void FixedUpdate()
        {
            if (HasPlayerMovement) { playerMovement.MovementFixedUpdate(); }
        }
    }
}
