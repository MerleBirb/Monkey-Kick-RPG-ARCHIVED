using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 1)]
public class Character : ScriptableObject
{
    /// CHARACTER OBJECT ///
    /// This scriptable object stores stats and other information about a character.
    
    /// BASIC INFORMATION ///
    public new string name = "Name"; /// the display name of the character. used for storing info.
    public string fullName = "Firstname Lastname"; /// the full name of the character, for story and description purposes.
    public int powerLevel = 0; /// the total experience, aka the powerlevel of the character. a measure that averages all their abilites to a
    /// numerical value.

    [TextArea(15, 20)]
    public string description; /// the description of the character; like a small summary of who they are, what they are, etc. height, weight, etc.

    public AudioClip[] talkSounds; /// the character's voice.
    public float maxPitch;
    public float minPitch;

    /// CHARACTER STATS ///
    public int level = 1; /// the character's threat level. how much of a threat are they?
    public int maxXP; /// the character's needed exp to level up to the next level.
    public int maxHP; /// the character's maximum health.
    public int maxEP; /// the character's maximum energy.
    public int strength; /// the character's physical strength. affects physical attack and enengy attack scaling. 
    public int intelligence; /// the character's brain power. affects magical attack and energy attack scaling.
}
