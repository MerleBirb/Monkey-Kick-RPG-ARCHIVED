using UnityEngine;

public class StatBoundaries : MonoBehaviour
{
    ////////// STAT BOUNDARIES //////////
    /// keeps the stats in check

    // store the player stats
    private PlayerBattleScript player;

    // first frame
    private void Awake()
    {
        player = GetComponent<PlayerBattleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHP < 0)
        {
            player.currentHP = 0;
        }

        if (player.currentHP > player.charStats.maxHP)
        {
            player.currentHP = player.charStats.maxHP;
        }

        if (player.currentEnergy < 0)
        {
            player.currentEnergy = 0;
        }

        if (player.currentEnergy > player.charStats.maxEnergy)
        {
            player.currentEnergy = player.charStats.maxEnergy;
        }
    }
}
