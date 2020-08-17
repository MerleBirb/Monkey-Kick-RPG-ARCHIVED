using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// PLAYER MOVEMENT //////////
    /// the script that allows the player to use their keyboard or controller to control their player

    // Vector3 that stores x, y, and z axis of movement
    private Vector3 moveDirection;

    // overworld physics stuff
    [SerializeField]
    private bool isGrounded = false;
    public LayerMask groundLayer;

    // interactivity variables
    public Transform groundCheck;
    public float footRadius = 0.25f;

    // speed variables that affect player movement
    [HideInInspector]
    public float currentMoveSpeed;
    public float moveSpeed = 2f;
    public float sprintSpeed = 4f;
    public float jumpHeight = 5f;
    private bool moving = false;

    // stores the player's rigidbody
    private Rigidbody rb;

    // store the animator
    public Animator animator;

    // store the walk dust
    public GameObject walkDust;
    private GameObject newWalkDust;

    // sounds
    public AudioSource[] audioSources;

    // store the character scriptable object
    public Character character;

    // Awake is called before anything
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
        CheckGround();
    }

    // FixedUpdate is called once per frame consistently
    private void FixedUpdate()
    {
        Movement();
    }

    // Checks input from the keyboard and mouse / controller
    private void CheckInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f || Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.5f)
        {
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
            moving = true;
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            moving = false;
        }

        if (LuaEnvironment.isPlayerInDialogue || MenuManager.state != MenuManager.Menus.MENU_UNOPENED)
        {
            animator.SetFloat("Speed", 0f);
            moving = false;
        }

        if (Input.GetButton("B_Button"))
        {
            currentMoveSpeed = sprintSpeed;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        if (isGrounded)
        {
            if (!LuaEnvironment.isPlayerInDialogue && MenuManager.state == MenuManager.Menus.MENU_UNOPENED)
            {
                if (Input.GetButtonDown("A_Button"))
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
                    audioSources[0].Play();
                    isGrounded = false;
                }
            }
        }

        if (!Input.GetButton("A_Button") && isGrounded && !moving)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }

        animator.speed = currentMoveSpeed / moveSpeed;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.z);

        if (moving)
        {
            animator.SetFloat("LastHorizontal", moveDirection.x);
            animator.SetFloat("LastVertical", moveDirection.z);
        }
    }

    // Calculates movement for the player using their Rigidbody2D
    private void Movement()
    {
        if (!LuaEnvironment.isPlayerInDialogue && MenuManager.state == MenuManager.Menus.MENU_UNOPENED)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, (transform.forward * moveDirection.z * currentMoveSpeed) +
                (transform.right * moveDirection.x * currentMoveSpeed) + (new Vector3(0f, rb.velocity.y, 0f)), 30f * Time.deltaTime);
        }

        if (isGrounded)
        {
            if (moving)
            {

                if (newWalkDust == null)
                {
                    newWalkDust = Instantiate(walkDust, groundCheck);

                    Destroy(newWalkDust, 1f / currentMoveSpeed);
                }

                if (!audioSources[1].isPlaying)
                {
                    audioSources[1].pitch = Random.Range((currentMoveSpeed / moveSpeed) * 1.0f, 
                        (currentMoveSpeed / moveSpeed) * 1.3f);
                    audioSources[1].Play();
                }
            }
        }
    }

    // checks to see if the feet touch ground :D foot check foot check hahah
    private void CheckGround()
    {
        Collider[] surfaces = Physics.OverlapSphere(groundCheck.position, footRadius, groundLayer);
        
        if(surfaces.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
