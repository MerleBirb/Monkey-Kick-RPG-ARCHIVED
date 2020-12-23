using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    /// TRIGGERS DIALOGUE ///
    /// triggers the dialogue state for the game

    /// VARIABLES ///
    // store the dialogue UI
    public GameObject dialogueUI;
    public LuaEnvironment lua;
    // store the dialogue
    [SerializeField]
    private string dialogueFile;
    // store the button
    public GameObject button;
    // store the player object
    public PlayerMovement player;
    // check whether to see if the player is in range or not
    public static bool playerInRange = false;
    // player controls

    /// FUNCTIONS ///
    // start activates in the beginning
    private void Start()
    {
        button.SetActive(false);
        playerInRange = false;
    }

    // update happens every frame
    public void Update()
    {
        if (player.pressedInteract)
        {
            if (playerInRange && !LuaEnvironment.inDialogue)
            {
                
                if (lua.loadFile != dialogueFile)
                {
                    lua.loadFile = dialogueFile;
                }

                dialogueUI.SetActive(true);
                button.SetActive(false);
                StartCoroutine(lua.Setup());
                LuaEnvironment.inDialogue = true;
                
            }

            player.pressedInteract = false;
        }
    }

    // trigger near the player
    private void OnTriggerEnter(Collider other)
    {
        if (!LuaEnvironment.inDialogue)
        {
            if (other.tag == "Player")
            {
                if (!playerInRange)
                {
                    button.SetActive(true);
                    playerInRange = true;
                }
            }
        }
    }

    // if the player stays in the zone
    private void OnTriggerStay(Collider other)
    {
        if (!LuaEnvironment.inDialogue)
        {
            if (other.tag == "Player")
            {
                button.SetActive(true);

                if (!playerInRange)
                {
                    playerInRange = true;
                }
            }
        }
    }

    // un trigger once the player moves away
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            button.SetActive(false);
            dialogueUI.SetActive(false);
            playerInRange = false;
            LuaEnvironment.inDialogue = false;
        }
    }
}
