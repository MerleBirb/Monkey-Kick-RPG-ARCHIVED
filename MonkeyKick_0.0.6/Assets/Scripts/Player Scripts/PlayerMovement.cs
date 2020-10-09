using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// PLAYER MOVEMENT SCRIPT ///
    /// This script handles how player characters interact with physics, move, and collide!
    /// thank you @ Catlike Coding for the awesome tutorial on this! I've been looking for help on
    /// perfect movement everywhere ;v; 10/9/2020
    /// PLACEHOLDER STUFF IS TO SIMULATE STUFF DURING THE TUTORIAL, DELETE LATER

    /// VARIABLES ///
    /// speed variables
    // maximum speed variable: changes how fast the object is moving
    [SerializeField]
    private float maxSpeed = 10f;
    // the velocity variable stores a vector that... well controls the velocity!
    private Vector3 velocity;
    // maximum acceleration variable, storing a value for the max acceleration
    [SerializeField]
    private float maxAcceleration = 10f;

    // placeholder stuff
    [SerializeField]
    private Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    /// FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    // PlayerInput takes the input from the user and applies it to the player's movements
    private void PlayerInput()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;
        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            newPosition = transform.localPosition;
        }
        transform.localPosition = newPosition;
    }
}
