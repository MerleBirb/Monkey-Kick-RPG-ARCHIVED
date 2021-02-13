using UnityEngine;
using Kryz.CharacterStats;

[System.Serializable]
public class Character
{
    /// CHARACTER OBJECT ///
    /// This scriptable object stores stats and other information about a character.
    
    /// BASIC INFORMATION ///
    public string name = "Name"; /// the display name of the character. used for storing info.
    public int totalEXP = 0; /// the total experience, aka the power level of the character. a measure that averages all their abilites to a
                             /// numerical value.

    [TextArea(15, 20)]
    public string description; /// the description of the character; like a small summary of who they are, what they are, etc. height, weight, etc.

    public AudioClip[] talkSounds; /// the character's voice.

    /// CHARACTER STATS ///
    public int level = 1; /// the character's threat level. how much of a threat are they?
    public int neededEXP; /// the character's needed exp to level up to the next level.
    public CharacterStat maxHP; /// the character's maximum health points. dont run out of these, or you'll get KO'd!
    public CharacterStat maxEP; /// the character's maximum energy points. use these to execute special skills!
    public CharacterStat attack; /// the character's attack points. the more of these you have, the more your punches will hurt! so will your counter attacks.
    public CharacterStat defense; /// the character's defense points. more defense means less damage taken!
    public CharacterStat specialAttack; /// the character's special attack points. its special cause it applies to special skills! more damage and effectiveness.
    public CharacterStat specialDefense; /// the character's special defense points. helps take less damage from special moves and less status chance.
    public CharacterStat speed; /// the character's speed points. affects when you go in the turn order.
    public CharacterStat luck; /// the character's luck points. are you a lucky fella? this weird stat can affect many things in different ways.
                               /// it cuts prices in shop by a percentage, increases crit damage, increases the chance of status effects from moves!
                               
    // the current amount of these points
    public int currentHP, currentEP;
}
