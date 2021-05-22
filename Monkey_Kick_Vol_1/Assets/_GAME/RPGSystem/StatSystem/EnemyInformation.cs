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
    public int EXPGiven;

    public override void OnValidate()
    {
        base.OnValidate();

        EXPGiven = Mathf.Clamp(EXPGiven, 0, statClamp);
    }
}
