using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    ////////// THE DIALOGUE //////////

    // create a queue to store the current sentences being used for the dialogue
    private Queue<string> sentences;

    // store the name text and dialogue text
    public Text nameText;
    public Text dialogueText;

    // store the animator for transitions
    public Animator anim;

    // store the current sound
    AudioSource talkSound;
    public AudioClip[] newTalkSounds;
    float newMinPitch;
    float newMaxPitch;

    // is the player in dialogue or not?
    public static bool playerInDialogue = false;

    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
        talkSound = GetComponent<AudioSource>();
        playerInDialogue = false;
    }

    // Update is called every frame
    private void Update()
    {
        if (playerInDialogue)
        {
            if(Input.GetButtonDown("Interact"))
            {
                DisplayNextSentence();
            }
        }
    }

    // begin the dialogue sequence
    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    // move on to the next sentence
    private void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // store sound clips
    public void StoreTalkingSound(Dialogue dialogue)
    {
        newTalkSounds = dialogue.talkSounds;
        newMinPitch = dialogue.minPitch;
        newMaxPitch = dialogue.maxPitch;
    }

    // make the letters appear one by one
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(playerInDialogue)
            {
                if(!talkSound.isPlaying)
                {
                    talkSound.clip = newTalkSounds[Random.Range(0, 2)];
                    talkSound.volume = Random.Range(newMinPitch, newMaxPitch);
                    talkSound.pitch = Random.Range(newMinPitch, newMaxPitch);
                    talkSound.Play();
                }           
            }

            dialogueText.text += letter;
            yield return null;
        }
    }

    // resetting the sentences
    private void EndDialogue()
    {
        playerInDialogue = false;
        anim.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }
}
