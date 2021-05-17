//===== ENEMY INFO =====//
/*
5/16/21
Description:
- Holds information specific to enemies.
- Derives from CharacterInfo.

*/

using System;
using UnityEngine;

[Serializable]
public class EnemyInfo : CharacterInfo
{
    /// STATS
    // experience gained by killing
    [SerializeField] private int EXPGained = 1;

    void OnValidate()
    {
        EXPGained = Mathf.Clamp(EXPGained, 0, int.MaxValue);
    }
}
