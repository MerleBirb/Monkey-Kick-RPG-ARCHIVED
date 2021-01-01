using UnityEngine;

public class Stats : MonoBehaviour
{
    /// STATS ///
    /// This simple script holds character stats... that's it.
    
    // stores the character stats
    public Character charStats;

    /// keeps hp and ep from going over or below bounds
    public void KeepPointsInCheck()
    {
        if (charStats.currentHP <= 0)
        {
            charStats.currentHP = 0;
        }

        if (charStats.currentHP > charStats.maxHP.Value)
        {
            charStats.currentHP = (int)charStats.maxHP.Value;
        }

        if (charStats.currentEP <= 0)
        {
            charStats.currentEP = 0;
        }

        if (charStats.currentEP > charStats.maxEP.Value)
        {
            charStats.currentEP = (int)charStats.maxEP.Value;
        }
    }
}
