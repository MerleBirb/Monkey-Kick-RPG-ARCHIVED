using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 1)]
public class Character : ScriptableObject
{
    public new string name = "Name";
    public AudioClip[] talkSounds;
    public float maxPitch;
    public float minPitch;
}
