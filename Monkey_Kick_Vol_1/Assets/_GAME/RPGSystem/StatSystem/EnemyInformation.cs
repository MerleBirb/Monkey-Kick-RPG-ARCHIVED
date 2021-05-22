//===== ENEMY INFORMATION =====//
/*
5/22/21
Description:
- Holds enemy and boss information.

Author: Merlebirb
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "New Character/New Enemy Character")]
public class EnemyInformation : CharacterInformation
{
    [Tooltip("Experience given to the player once the enemy is defeated.")]
    public int EXPGiven;

    public override void OnValidate()
    {
        base.OnValidate();

        EXPGiven = Mathf.Clamp(EXPGiven, 0, statClamp);
    }
}
