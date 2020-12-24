namespace Kryz.CharacterStats
{
    /// ENUM ///
    /// this enum covers the types of stat modifiers
    public enum StatModType
    {
        FLAT = 100,
        PERCENT_ADD = 200,
        PERCENT_MULT = 300
    }

    public class StatModifier
    {
        /// STAT MODIFIER ///
        /// A custom data type that modifies character stats 
        /// Credit goes to @Kryzarel for the awesome tutorials! 12/23/2020

        /// VARIABLES /// 

        // value for the modifier
        public readonly float Value;

        // enum for the type of stat modifier
        public readonly StatModType Type;

        // order of whether flat bonus or percent bonus comes first
        public readonly int Order;

        // object variable can literally be any type, super useful because it can tell where a modifier came from
        public readonly object Source;

        /// CONSTRUCTOR ///
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
