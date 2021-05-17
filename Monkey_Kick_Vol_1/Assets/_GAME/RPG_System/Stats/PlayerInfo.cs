//===== PLAYER INFO =====//
/*
5/16/21
Description:
- Holds information specific to players.
- Derives from CharacterInfo.

*/

using System;
using UnityEngine;

[Serializable]
public class PlayerInfo : CharacterInfo
{
    /// STATS
    // experience points
    public int maxEXP = 1;
    public int currentEXP = 0;
    public int totalEXP = 0;

    public override void OnValidate()
    {
        base.OnValidate();

        maxEXP = Mathf.Clamp(maxEXP, 1, int.MaxValue);
        currentEXP = Mathf.Clamp(currentEXP, 0, maxEXP);
    }
}
