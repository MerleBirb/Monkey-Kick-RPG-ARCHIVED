using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int currentXP;
    public int maxXP;
    public int currentHP;
    public int maxHP;
    public int currentEP;
    public int maxEP;
    public int strength;
    public int intelligence;
    public int defense;
    public int speed;
    public int luck;
    public float[] position;
    public PlayerData (PlayerBattleScript player)
    {
        level = player.charStats.level;
        currentXP = player.currentXP;
        maxXP = player.charStats.maxXP;
        currentHP = player.currentHP;
        maxHP = player.charStats.maxHP;
        currentEP = player.currentEnergy;
        maxEP = player.charStats.maxEnergy;
        strength = player.charStats.strength;
        intelligence = player.charStats.intelligence;
        defense = player.charStats.defense;
        speed = player.charStats.speed;
        luck = player.charStats.luck;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
