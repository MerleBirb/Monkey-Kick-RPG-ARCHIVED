//===== INT VARIABLE =====//
/*
6/2/21
Description:
- Scriptable Object version of an int.

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick
{
    [CreateAssetMenu(menuName = "New Variable/Integer", fileName = "Int")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}