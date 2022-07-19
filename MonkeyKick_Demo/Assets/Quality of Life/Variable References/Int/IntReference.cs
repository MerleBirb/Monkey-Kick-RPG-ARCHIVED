//===== INT REFERENCE =====//
/*
6/2/21
Description:
- Reference to the Scriptable Object version of an int.

Author: Merlebirb
*/

using System;

namespace MonkeyKick
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public IntReference()
        {
            UseConstant = true;
        }

        public IntReference(int value) : this()
        {
            ConstantValue = value;
        }

        public int Int
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}