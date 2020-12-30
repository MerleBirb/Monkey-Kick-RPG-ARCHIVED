using UnityEngine;

public class Stats : MonoBehaviour
{
    /// STATS ///
    /// This simple script holds character stats... that's it.
    
    // stores the character stats
    public Character charStats;

    // the current amount of these points
    [HideInInspector]
    public int currentHP, currentEP;

    /// keeps hp and ep from going over or below bounds
    public void KeepPointsInCheck()
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
        }

        if (currentHP > charStats.maxHP.Value)
        {
            currentHP = (int)charStats.maxHP.Value;
        }

        if (currentEP <= 0)
        {
            currentEP = 0;
        }

        if (currentEP > charStats.maxEP.Value)
        {
            currentEP = (int)charStats.maxEP.Value;
        }
    }
}
