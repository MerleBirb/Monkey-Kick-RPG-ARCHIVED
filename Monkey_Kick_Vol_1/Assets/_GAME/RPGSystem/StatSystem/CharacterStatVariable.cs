//===== CHARACTER STAT VARIABLE =====//
/*
5/22/21
Description: 
- Saves a Character Stat as a Scriptable Object.
- Special thanks to Ryan Hipple for this approach.

*/

using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "New Stat")]
public class CharacterStatVariable : ScriptableObject
{
    public CharacterStat Value;

    private void OnValidate()
    {
        Value.BaseValue = Mathf.Clamp(Value.BaseValue, 1, 9999);
    }
}
