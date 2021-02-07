using UnityEngine;

public class Stats : MonoBehaviour
{
    /// STATS ///
    /// This simple script holds character stats... that's it.
    
    // stores the character stats
    public Character charStats;

    /// loads stats onto the game manager player party
    private void Awake()
    {
        if (GameManager.Instance.PlayerParty[0] != charStats)
        {
            charStats = GameManager.Instance.PlayerParty[0];
        }
    }

    /// keeps hp and ep from going over or below bounds
    private void OnValidate()
    {
        charStats.currentHP = Mathf.Clamp(charStats.currentHP, 0, (int)charStats.maxHP.Value);
        charStats.currentEP = Mathf.Clamp(charStats.currentEP, 0, (int)charStats.maxEP.Value);
    }
}
