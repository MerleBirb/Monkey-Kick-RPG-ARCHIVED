using System;
using Godot;

namespace Merlebirb.TurnBasedSystem
{
    //===== CHARACTER =====//
    /*
    4/2/21
    Description: Holds character information like name, stats, etc.
    
    */

    [Serializable]
    public class Character : Godot.Resource
    {
        [Export] public string name = "New Character";
        [Export] private string description = "Description";
        [Export] public int level = 1;
        [Export] public int totalXP = 0; // total experience points
        [Export] public int neededXP = 1; // needed experience points for next level
        [Export] public CharacterStat maxHP = new CharacterStat(); // maximum hit points
        [Export] public int currentHP;
        [Export] public CharacterStat maxKP = new CharacterStat(); // maximum ki points (ki is the energy / mana system)
        [Export] public int currentKP;
        [Export] public CharacterStat attack = new CharacterStat(); // increases physical damage dealt
        [Export] public CharacterStat defense = new CharacterStat(); // reduces physical damage taken
        [Export] public CharacterStat specialAttack = new CharacterStat(); // increases energy damage dealt 
        [Export] public CharacterStat specialDefense = new CharacterStat(); // reduces energy damage taken
        [Export] public CharacterStat speed = new CharacterStat(); // determines turn order
        [Export] public CharacterStat swag = new CharacterStat(); // determines crit ratio and store prices
    }
}
