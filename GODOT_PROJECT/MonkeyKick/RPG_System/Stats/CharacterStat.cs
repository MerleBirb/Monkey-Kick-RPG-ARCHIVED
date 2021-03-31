using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Merlebirb.TurnBasedSystem
{
    //===== CHARACTER STAT =====//
    /*
    3/30/21
    Description: A character stat is a value that stores information about a certain stat, like hit points or attack power.

    Thank you @ Kryzarel for the wonderful tutorial.
    */

    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;
        public virtual float Value
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }
        protected bool isDirty = true; // if the value has been modified
        protected float _value;
        protected float lastBaseValue = float.MinValue;

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }
        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) { return -1; }
            else if (a.Order > b.Order) { return 1; }
            return 0; // if (a.Order == b.Order)
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

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

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModeType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModeType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModeType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModeType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }

                finalValue += statModifiers[i].Value;
            }

            return (float)Math.Round(finalValue, 2);
        }
    }

}

