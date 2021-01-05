using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kryz.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        /// CHARACTER STAT ///
        /// A custom data type for a character stat
        /// Credit goes to @Kryzarel for the awesome tutorials! 12/23/2020

        /// VARIABLES ///

        // base value is the unmodified starting value for a stat
        public float BaseValue;

        // current value of the stat
        public virtual float Value
        {
            get
            {
                if (isDirty)
                {
                    lastBasevalue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        // the value is "dirty" when it needs to be recalculated, bool for that here
        protected bool isDirty = true;
        protected float _value; // temp value
        protected float lastBasevalue = float.MinValue; // temp variable

        // a list of all the stat modifiers that have been applied to the stat
        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        /// CONSTRUCTOR ///
        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        /// FUNCTIONS ///

        /// adds a modifier to the stat
        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        /// removes all modifiers from specific source
        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        /// compares and sorts modifiers
        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }

            return 0; // if (a.Order == b.Order)
        }

        /// remove a modifier to the stat
        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        /// combine stat modifiers and calculate the final value of the stat
        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            // go through every stat modifier
            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.FLAT)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PERCENT_ADD)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PERCENT_ADD)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PERCENT_MULT)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}
