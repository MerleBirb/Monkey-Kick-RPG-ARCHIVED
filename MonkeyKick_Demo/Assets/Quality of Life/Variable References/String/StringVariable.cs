//===== STRING VARIABLE =====//
/*
7/13/21
Description:
- Scriptable Object version of a string.

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick
{
    [CreateAssetMenu(menuName = "New Variable/String", fileName = "String")]
    public class StringVariable : ScriptableObject
    {
        public string Value;
    }
}
