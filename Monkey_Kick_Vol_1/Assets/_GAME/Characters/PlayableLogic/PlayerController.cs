//===== PLAYER CONTROLLER =====//
/*
5/11/21
Description: 
- Controls all player logic.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PlayableController
{
    [SerializeField] private PlayerOverworld playerOverworld; // overworld logic
    //[SerializeField] private PlayerBattle playerBattle; // battle logic

    #region COMPONENTS

    private PlayerInput input; // stores the controls
    private Rigidbody rb;

    #endregion

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // add new gamestate system
        playerOverworld.SetComponents(rb, input, transform);
        playerOverworld.InitiateControls();
    }

    private void Update()
    {
        // add new gamestate system
        playerOverworld.CheckForPlayerInput();
        playerOverworld.CheckIfGravityShouldApply();
    }

    private void FixedUpdate()
    {
        // add new gamestate system
        playerOverworld.Movement();
        playerOverworld.UpdatePhysicsCount();
        playerOverworld.ClearPhysicsCount();
    }
}