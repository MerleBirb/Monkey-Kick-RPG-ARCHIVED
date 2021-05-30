//===== STAT MODIFIER =====//
/*
5/16/21
Description:
- Modifies a character stat.
- Special thanks to @Kryzarel on youtube for the amazing tutorial.

Author: Kryzarel
*/

namespace MonkeyKick.Stats
{
    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}