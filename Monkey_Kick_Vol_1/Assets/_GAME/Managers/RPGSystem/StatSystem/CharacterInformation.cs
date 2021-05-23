//===== CHARACTER INFORMATION =====//
/*
5/22/21
Description:
- Holds character information, abstract class

Author: Merlebirb
*/

using UnityEngine;

public class CharacterInformation : ScriptableObject
{
    protected int statClamp = 9999;

    public string CharacterName = "New Character";
    [Multiline] public string Description;
    public int Level;

    public CharacterStatReference MaxHP;
    public CharacterStatReference CurrentHP;
    [HideInInspector] public bool isAlive = true;

    public CharacterStatReference MaxKP; // energy / mana system
    public CharacterStatReference CurrentKP;

    public CharacterStatReference Muscle; // physical strength
    public CharacterStatReference Toughness; // physical defense
    public CharacterStatReference Smarts; // energy attack
    public CharacterStatReference Tenacity; // energy defense
    public CharacterStatReference Speed; // speed... what else?
    public CharacterStatReference Swag; // luck / crit chance

    public virtual void OnValidate()
    {
        Level = Mathf.Clamp(Level, 1, 100);

        MaxHP.Value.BaseValue = Mathf.Clamp(MaxHP.Value.BaseValue, 1, statClamp);
        CurrentHP.Value.BaseValue = Mathf.Clamp(CurrentHP.Value.BaseValue, 0, MaxHP.Value.BaseValue);

        MaxKP.Value.BaseValue = Mathf.Clamp(MaxKP.Value.BaseValue, 1, statClamp);
        CurrentKP.Value.BaseValue = Mathf.Clamp(CurrentKP.Value.BaseValue, 0, MaxKP.Value.BaseValue);

        Muscle.Value.BaseValue = Mathf.Clamp(Muscle.Value.BaseValue, 1, statClamp);
        Toughness.Value.BaseValue = Mathf.Clamp(Toughness.Value.BaseValue, 1, statClamp);
        Smarts.Value.BaseValue = Mathf.Clamp(Smarts.Value.BaseValue, 1, statClamp);
        Tenacity.Value.BaseValue = Mathf.Clamp(Tenacity.Value.BaseValue, 1, statClamp);
        Speed.Value.BaseValue = Mathf.Clamp(Speed.Value.BaseValue, 1, statClamp);
        Swag.Value.BaseValue = Mathf.Clamp(Swag.Value.BaseValue, 1, statClamp);
    }
}
