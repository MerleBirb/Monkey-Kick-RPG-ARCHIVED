//===== CHARACTER INFO =====//
/*
5/16/21
Description:
- Holds the character's information, such as:
- Name, Description
- Stats

*/

using System;
using UnityEngine;

public abstract class CharacterInfo : ISerializationCallbackReceiver
{
    protected int statLimit = 9999;

    /// GENERAL INFO
    public string characterName = "Character";
    [TextArea(15, 15)] public string description = "This guy is cool.";
    public int level = 1;
    
    /// STATS

    // hit points
    public CharacterStat maxHP;
    public CharacterStat currentHP;
    
    // ki points
    public CharacterStat maxKP;
    public CharacterStat currentKP;

    public CharacterStat muscle; // physical attack
    public CharacterStat toughness; // physical defense
    public CharacterStat smarts; // energy attack
    public CharacterStat tenacity; // energy defense
    public CharacterStat speed; // speed... what else?
    public CharacterStat swag; // luck

    public virtual void OnValidate()
    {
        level = Mathf.Clamp(level, 1, 100);

        maxHP.BaseValue = Mathf.Clamp(maxHP.BaseValue, 1, statLimit);
        currentHP.BaseValue = Mathf.Clamp(currentHP.BaseValue, 1, maxHP.BaseValue);

        maxKP.BaseValue = Mathf.Clamp(maxKP.BaseValue, 1, statLimit);
        currentKP.BaseValue = Mathf.Clamp(currentKP.BaseValue, 1, maxKP.BaseValue);

        muscle.BaseValue = Mathf.Clamp(muscle.BaseValue, 1, statLimit);
        toughness.BaseValue = Mathf.Clamp(toughness.BaseValue, 1, statLimit);
        smarts.BaseValue = Mathf.Clamp(smarts.BaseValue, 1, statLimit);
        tenacity.BaseValue = Mathf.Clamp(tenacity.BaseValue, 1, statLimit);
        speed.BaseValue = Mathf.Clamp(speed.BaseValue, 1, statLimit);
        swag.BaseValue = Mathf.Clamp(swag.BaseValue, 1, statLimit);
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() => this.OnValidate();
    void ISerializationCallbackReceiver.OnAfterDeserialize() { }
}
