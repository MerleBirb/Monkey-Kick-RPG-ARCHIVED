//===== CHARACTER STAT =====//
/*
5/16/21
Description:
- A stat value, self explanatory.
- Special thanks to @Kryzarel on youtube for the amazing tutorial.

*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonkeyKick.Stats
{
    [Serializable]
    public class CharacterStat
    {
        public int BaseValue;

        public int Value
        {
            get
            {
                if (_isDirty || BaseValue != _lastBaseValue)
                {
                    _lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    _isDirty = false;
                }

                return _value;
            }
            set
            {
                if (_isDirty || BaseValue != _lastBaseValue)
                {
                    _lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    _isDirty = true;
                }

                _value = value;
            }
        }

        protected bool _isDirty = true;
        protected int _value;
        protected int _lastBaseValue = int.MinValue;

        protected readonly List<StatModifier> _statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            _statModifiers = new List<StatModifier>();
            StatModifiers = _statModifiers.AsReadOnly();
        }

        public CharacterStat(int baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            _isDirty = true;
            _statModifiers.Add(mod);
            _statModifiers.Sort(CompareModifierOrder);
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) return -1;
            else if (a.Order > b.Order) return 1;
            return 0; // if (a.Order == b.Order)
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (_statModifiers.Remove(mod))
            {
                _isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    _isDirty = true;
                    didRemove = true;
                    _statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual int CalculateFinalValue()
        {
            float finalValue = (float)BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier mod = _statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (int)Math.Round(finalValue, 4);
        }
    }
}