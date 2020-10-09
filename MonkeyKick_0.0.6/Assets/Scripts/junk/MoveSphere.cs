using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    // maximum speed variable: changes how fast the object is moving
    [SerializeField]
    private float maxSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 velocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        Vector3 displacement = velocity * Time.deltaTime;

        transform.localPosition += displacement;
    }
}
