//===== CHARACTER STAT REFERENCE =====//
/*
5/22/21
Description:
- Saves a reference of the Scriptable Object of a Character Stat.
- Special thanks to Ryan Hipple for this approach.

*/

using System;

namespace MonkeyKick.Character
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

        public CharacterStat Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}