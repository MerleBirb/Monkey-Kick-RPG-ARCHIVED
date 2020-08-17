using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public PlayerBattleScript player;

    // saves the game
    public void Save()
    {
        SaveSystem.Save(player);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.Load();

        player.charStats.level = data.level;
        player.currentXP = data.currentXP;
        player.charStats.maxXP = data.maxXP;
        player.currentHP = data.currentHP;
        player.charStats.maxHP = data.maxHP;
        player.currentEnergy = data.currentEP;
        player.charStats.maxEnergy = data.maxEP;
        player.charStats.strength = data.strength;
        player.charStats.intelligence = data.intelligence;
        player.charStats.defense = data.defense;
        player.charStats.speed = data.speed;
        player.charStats.luck = data.luck;

        Vector3 playerPos;
        playerPos.x = data.position[0];
        playerPos.y = data.position[1];
        playerPos.z = data.position[2];
        player.transform.position = playerPos;
    }
}
