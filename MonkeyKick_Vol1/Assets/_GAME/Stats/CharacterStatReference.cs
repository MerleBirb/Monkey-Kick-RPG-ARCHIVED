//===== CHARACTER STAT REFERENCE =====//
/*
5/22/21
Description:
- Saves a reference of the Scriptable Object of a Character Stat.
- Special thanks to Ryan Hipple for this approach.

Author: Merlebirb
*/

using System;

namespace MonkeyKick.Stats
{
    [Serializable]
    public class CharacterStatReference
    {
        public bool UseConstant = true;
        public CharacterStat ConstantValue;
        public CharacterStatVariable Variable;

        public CharacterStatReference()
        {
            UseConstant = true;
        }

        public CharacterStatReference(CharacterStat value) : this()
        {
            ConstantValue = value;
        }

        public CharacterStat Stat
        {
            get { return UseConstant ? ConstantValue : Variable.Stat; }
        }

        public void ChangeStat(int changeByValue)
        {
            if (UseConstant) { ConstantValue.BaseValue += changeByValue; }
            else { Variable.Stat.BaseValue += changeByValue; }
        }
    }
}