using UnityEngine;

public class Stats : MonoBehaviour
{
    /// STATS ///
    /// This simple script holds character stats... that's it.
    
    // stores the character stats
    public Character charStats;

    /// keeps hp and ep from going over or below bounds
    private void OnValidate()
    {
        charStats.currentHP = Mathf.Clamp(charStats.currentHP, 0, (int)charStats.maxHP.Value);
        charStats.currentEP = Mathf.Clamp(charStats.currentEP, 0, (int)charStats.maxEP.Value);
    }
}
