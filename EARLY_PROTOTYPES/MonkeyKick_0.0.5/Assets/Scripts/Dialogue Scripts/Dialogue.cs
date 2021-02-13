using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite buttonImage;

    [TextArea(3, 10)]
    public string[] sentences;

    public AudioClip[] talkSounds;
    public float minPitch;
    public float maxPitch;
}
