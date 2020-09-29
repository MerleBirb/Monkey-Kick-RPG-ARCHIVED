using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuaCommands : MonoBehaviour
{
    ////////// LUA COMMANDS //////////
    /// this is an important script, as it sets up the basic commands for importing Lua into dialogue.

    // store the lua environment
    [SerializeField]
    private LuaEnvironment lua;

    // singleton of the lua commands object
    private static LuaCommands instance;

    // store the ui text for the dialogue
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Text nameText;
    private string typingText;
    private bool doneTyping = false;

    // store the button handler
    private ButtonHandler buttons;

    // store all interactable objects in the scene
    public List<GameObject> talkObjects;

    // store interactable object tags
    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    private string interactableTag = "Interactable";

    // store the current sound
    private AudioSource talkSound;
    private AudioClip[] newTalkSounds = null;
    private float newMinPitch = 0.0f;
    private float newMaxPitch = 0.0f;

    //// store interactable object scripts
    //private PlayerMovement player;
    //private EnemyBattleScript enemy;

    // Awake starts right when the scene starts or when the object begins existing
    private void Awake()
    {
        instance = this;
        talkSound = GetComponent<AudioSource>();

        //player = null;
        //enemy = null;
    }

    // Start is called on the first frame
    private void Start()
    {
        buttons = FindObjectOfType<ButtonHandler>();
        lua = FindObjectOfType<LuaEnvironment>();

        talkObjects = new List<GameObject>();
        talkObjects.AddRange(GameObject.FindGameObjectsWithTag(playerTag));
        talkObjects.AddRange(GameObject.FindGameObjectsWithTag(enemyTag));
        talkObjects.AddRange(GameObject.FindGameObjectsWithTag(interactableTag));
    }

    // sets the line of dialogue
    public static void SetText(string textString)
    {
        instance.doneTyping = false;
        instance.typingText = textString;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.TypeSentence(instance.typingText));
    }

    // letter animation
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (!talkSound.isPlaying)
            {
                talkSound.clip = newTalkSounds[Random.Range(0, newTalkSounds.Length)];
                talkSound.pitch = Random.Range(newMinPitch, newMaxPitch);
                talkSound.Play();
            }

            dialogueText.text += letter;
            doneTyping = true;

            yield return new WaitForSeconds(0.02f);
        }
    }

    // show the buttons...
    public static void ShowButtons(string btn1TextString, string btn2TextString)
    {
        if (instance.doneTyping)
        {
            instance.buttons.ShowButtons(btn1TextString, btn2TextString);
        }
    }

    // turn off the selector
    public static void ToggleChoosingChoice(bool toggle)
    {
        instance.buttons.isChoosingChoice = toggle;
    }

    // change the person talking. depending on whos talking, different sounds will come up, and the text hue will slightly change!
    public static void SetCharacterName(string name)
    {
        instance.lua.LuaGameState.CharacterName = name;
        instance.nameText.text = name;
        
        for (int i = 0; i < instance.talkObjects.Count; i++)
        {
            if (instance.talkObjects[i].tag == instance.playerTag)
            {
                if (instance.talkObjects[i].GetComponent<PlayerMovement>().character.name == name)
                {
                    instance.newTalkSounds = instance.talkObjects[i].GetComponent<PlayerMovement>().character.talkSounds;
                    instance.newMinPitch = instance.talkObjects[i].GetComponent<PlayerMovement>().character.minPitch;
                    instance.newMaxPitch = instance.talkObjects[i].GetComponent<PlayerMovement>().character.maxPitch;
                }
            }
            else if (instance.talkObjects[i].tag == instance.enemyTag)
            {
                if (instance.talkObjects[i].GetComponent<EnemyBattleScript>().charStats.name == name)
                {
                    instance.newTalkSounds = instance.talkObjects[i].GetComponent<EnemyBattleScript>().charStats.talkSounds;
                    instance.newMinPitch = instance.talkObjects[i].GetComponent<EnemyBattleScript>().charStats.minPitch;
                    instance.newMaxPitch = instance.talkObjects[i].GetComponent<EnemyBattleScript>().charStats.maxPitch;
                }
            }
            else if (instance.talkObjects[i].tag == instance.interactableTag)
            {
                if (instance.talkObjects[i].GetComponent<InteractableScript>().inName == name)
                {
                    instance.newTalkSounds = instance.talkObjects[i].GetComponent<InteractableScript>().talkSounds;
                    instance.newMinPitch = instance.talkObjects[i].GetComponent<InteractableScript>().minPitch;
                    instance.newMaxPitch = instance.talkObjects[i].GetComponent<InteractableScript>().maxPitch;
                }
            }
        }
    }
}
