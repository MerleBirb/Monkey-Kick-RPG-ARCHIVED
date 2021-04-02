using System.Collections.Generic;

namespace Merlebirb.TurnBasedSystem
{
    //===== STAT MODIFIER =====//
    /*
    3/30/21
    Description: A stat modifier changes the value of a character stat in certain ways.

    Thank you @ Kryzarel for the wonderful tutorial.
    */

    public enum StatModeType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,

    }

    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModeType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, StatModeType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public StatModifier(float value, StatModeType type) : this (value, type, (int)type, null) {}
        public StatModifier(float value, StatModeType type, int order) : this (value, type, order, null) {}
        public StatModifier(float value, StatModeType type, object source) : this (value, type, (int)type, source){}
    }

}
