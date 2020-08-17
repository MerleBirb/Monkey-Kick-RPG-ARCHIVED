using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    ////////// ACTIVATE DIALOGUE //////////
    /// triggers the dialogue state for the game
    
    // store the dialogue UI
    public GameObject dialogueUI;
    public LuaEnvironment lua;

    // store the button
    public GameObject button;

    // check whether to see if the player is in range or not
    private bool playerInRange = false;

    // start activates in the beginning
    private void Start()
    {
        button.SetActive(false);
        playerInRange = false;
    }

    // update happens every frame
    private void Update()
    {
        if(playerInRange && !LuaEnvironment.isPlayerInDialogue)
        {
            if(Input.GetButtonDown("Y_Button"))
            {
                dialogueUI.SetActive(true);
                button.SetActive(false);
                StartCoroutine(lua.Setup());
                LuaEnvironment.isPlayerInDialogue = true;
            }
        }
    }

    // trigger near the player
    private void OnTriggerEnter(Collider other)
    {
        if (!LuaEnvironment.isPlayerInDialogue)
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
        if (!LuaEnvironment.isPlayerInDialogue)
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
        if(other.tag == "Player")
        {           
            button.SetActive(false);
            dialogueUI.SetActive(false);
            playerInRange = false;
            LuaEnvironment.isPlayerInDialogue = false;
        }
    }
}
