using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    ////////// CHARACTER DESCRIPTION //////////  
    public new string name; // what name or nickname does the character go by?
    public string fullName; // the character's full name

    [TextArea(15, 20)]
    public string description; // a description of the character

    public AudioClip[] talkSounds; // store all the character sounds
    public float minPitch; // minimim sound pitch
    public float maxPitch; // maximum sound pitch

    ////////// CHARACTER COMBAT STATS //////////
    public int level; // what's the character's level?
    public int maxXP; // how much experience does the character require to level up?
    public int XPValue; // how much this character is worth in experience
    public int maxHP; // how many punches can the character take?
    public int maxEnergy; // how many flashy moves can the character use?
    public int strength; // how much damage does this character's punch deal?
    public int intelligence; // how much damage does this character's big brain spells deal?
    public int defense; // how much damage reduction does this character have?
    public int speed; // how fast does this character go, affects turn order?
    public int luck; // what's the character's chance of hitting a lucky critical hit?
}
