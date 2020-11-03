using System.Collections.Generic;
using UnityEngine;

public class PDawgAnimations : MonoBehaviour
{
    /// P-DAWG ANIMATIONS ///
    /// This script controls P-Dawg's animations. Simple! Some animations (like attack or skill ones) are controlled
    /// directly controlled by their respective script. Thank you @ Lost Relic Games for your amazing tutorials! 11/1/2020

    /// VARIABLES ///
    // store the animator and the player movement script
    private Animator anim;
    private PlayerMovement player;

    // store the sound effects
    public List<AudioClip> soundClips = new List<AudioClip>();
    private AudioSource source;

    private enum Sounds : int
    {
        WALK = 0,
        JUMP = 1
    }

    // is the character moving?
    private bool moving;

    // store the current state name
    private string currentState;    

    /// FUNCTIONS ///
    /// Awake is called when the object activates
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerMovement>();
        source = GetComponent<AudioSource>();
        moving = false;
    }

    /// FixedUpdate is called once per frame at a constant framerate
    void FixedUpdate()
    {
        UpdateFace();
        UpdateSounds();
        CleanSounds();
    }

    /// UpdateFace updates which direction the player is facing
    private void UpdateFace()
    {
        float maxInputX;
        float maxInputY;

        if (Mathf.Abs(player.playerInput.x) > 0 || Mathf.Abs(player.playerInput.y) > 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (player.playerInput.x > 0)
        {
            maxInputX = 1;
        }
        else if (player.playerInput.x < 0)
        {
            maxInputX = -1;
        }
        else
        {
            maxInputX = 0;
        }

        if (player.playerInput.y > 0)
        {
            maxInputY = 1;
        }
        else if (player.playerInput.y < 0)
        {
            maxInputY = -1;
        }
        else
        {
            maxInputY = 0;
        }

        anim.SetFloat("Horizontal", maxInputX);
        anim.SetFloat("Vertical", maxInputY);
    }

    /// UpdateSounds keeps which sounds are playing in check
    private void UpdateSounds()
    {
        if (player.OnGround)
        {
            if (moving && !source.isPlaying)
            {
                source.clip = soundClips[(int)Sounds.WALK];
                source.pitch = Random.Range(1.5f, 1.8f);
                source.volume = 0.05f;
                source.Play();
            }

            if (player.pressedJump)
            {
                source.clip = soundClips[(int)Sounds.JUMP];
                source.pitch = 1.0f;
                source.volume = 0.5f;
                source.Play();
            }
        }

    }

    /// CleanSounds cleans the source.
    private void CleanSounds()
    {
        if (!source.isPlaying)
        {
            source.clip = null;
            source.pitch = 1.0f;
            source.loop = false;
            source.volume = 0.5f;
            source.Stop();
        }
    }

    /// ChangeAnimationState updates and changes the current animation playing
    private void ChangeAnimationState(string newState)
    {
        // the "guard": stops animation from interrupting itself
        if(currentState == newState)
        {
            return;
        }

        // play the animation
        anim.Play(newState);

        // update the currentState
        currentState = newState;
    }
}
