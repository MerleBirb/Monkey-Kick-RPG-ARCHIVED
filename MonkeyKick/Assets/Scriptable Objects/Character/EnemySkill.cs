using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : ScriptableObject
{
    ////////// ENEMY SKILL CLASS //////////
    /// this scriptable object class is the basis for all enemy battle skills in the game

    ////////// DESCRIPTION //////////
    // describes the skill
    public string skillName = "New Skill";
    [TextArea(10, 15)]
    public string skillDescription = "This skill does something.";

    ////////// ATTACK PRESENTATION //////////
    // the audio, the animation, which enemy is doing the move, etc
    public EnemyBattleScript enemy;
    public AudioSource audioSource;
    public AudioClip[] hitSound;

    // do the ability
    public abstract void SkillAction(EnemyBattleScript user);
}
