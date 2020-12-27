using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CatlikeCoding.Movement;

public class ButtonHandler : MonoBehaviour
{
    ////////// THE DIALOGUE BUTTON HANDLER //////////
    /// this script handles button presses to keep the dialogue going

    // store the environment
    private LuaEnvironment lua;
    private LuaCommands commands;

    // store the text options for the buttons
    [SerializeField]
    private GameObject buttonParent;
    [SerializeField]
    private Text button1Text;
    [SerializeField]
    private Text button2Text;

    // store the horizontal and vertical axis
    private float moveX;
    private float moveZ;

    // store the dialogue choice
    [SerializeField]
    private int dialogueSelect = 0;
    private bool stickPressed = false;
    public bool fastForward = false;

    // (placeholder) store the buttons being used
    private List<GameObject> dialogueChoices = new List<GameObject>();
    private string dialogueChoiceTag = "DialogueChoice";
    public Canvas dialogueManager;
    public GameObject selector;
    private GameObject currentSelector;

    private List<RectTransform> choiceTransforms = new List<RectTransform>();

    // are you selecting a choice or not?
    public bool isChoosingChoice = false;

    public GameObject player;

    // Start is called on the first frame
    private void Start()
    {
        lua = FindObjectOfType<LuaEnvironment>();
        commands = FindObjectOfType<LuaCommands>();
        dialogueChoices.AddRange(GameObject.FindGameObjectsWithTag(dialogueChoiceTag));
        buttonParent.SetActive(false);
        isChoosingChoice = false;

        for (int i = 0; i < dialogueChoices.Count; i++)
        {
            choiceTransforms.Add((RectTransform)dialogueChoices[i].transform);
        }

    }

    // Update is called every frame
    private void Update()
    {
        CheckInput();

        if (!isChoosingChoice)
        {
            if (currentSelector != null)
            {
                Destroy(currentSelector);
            }
        }
    }

    // FixedUpdate is called every frame at a fixed rate
    private void FixedUpdate()
    {
        //NavigateMenu();
        //MoveSelector();
    }

    // sets the text for the buttons
    public void ShowButtons(string btn1TextString, string btn2TextString)
    {
        button1Text.text = btn1TextString;
        button2Text.text = btn2TextString;
        buttonParent.gameObject.SetActive(true);
    }

    // Checks the button pressed
    private void CheckInput()
    {
        //moveX = Input.GetAxisRaw("Horizontal");
        //moveZ = Input.GetAxisRaw("Vertical");

        if(player.GetComponent<PlayerMovement>().pressedInteract)
        {
            if (commands.doneTyping)
            {
                Debug.Log("Choice selected " + dialogueSelect);
                lua.LuaGameState.ChoiceSelected = dialogueSelect + 1;

                buttonParent.SetActive(false);
                player.GetComponent<PlayerMovement>().pressedInteract = false;

                lua.AdvanceScript();
            }
            else
            {
                commands.FinishSentence();
            }
        }
    }

    // navigate the dialogue
    private void NavigateMenu()
    {
        if (isChoosingChoice)
        {
            if (moveZ > 0.6)
            {
                if (!stickPressed)
                {
                    dialogueSelect++;
                    stickPressed = true;
                }
            }
            else if (moveZ < -0.6)
            {
                if (!stickPressed)
                {
                    dialogueSelect--;
                    stickPressed = true;
                }
            }
            else
            {
                stickPressed = false;
            }

            if (dialogueSelect > dialogueChoices.Count - 1)
            {
                dialogueSelect = 0;
            }

            if (dialogueSelect < 0)
            {
                dialogueSelect = dialogueChoices.Count - 1;
            }
        }
        else
        {
            dialogueSelect = 0;
        }
    }

    // create the selector and move it depending on the player's choice
    private void MoveSelector()
    {
        if (currentSelector == null)
        {
            currentSelector = Instantiate(selector, dialogueManager.transform);
        }
               
        if(currentSelector != null)
        {
            currentSelector.transform.position = 
                new Vector3(dialogueChoices[dialogueSelect].transform.position.x - (choiceTransforms[dialogueSelect].rect.width * 1.4f),
                dialogueChoices[dialogueSelect].transform.position.y, dialogueChoices[dialogueSelect].transform.position.z);
        }
    }

    // checks to see if buttons should be visible or not
    public bool AreButtonsVisible()
    {
        return buttonParent.gameObject.activeSelf;
    }
}
