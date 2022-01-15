// Merle Roji
// 1/15/22

using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace MonkeyKick.UserInterface
{
    [MoonSharpUserData]
    public class LuaState
    {
        private HashSet<string> _flags;
        private string _currentName;
        private int _choiceSelected;

        [MoonSharpHidden]
        public LuaState()
        {
            _flags = new HashSet<string>();
        }

        public string CurrentName
        {
            get => _currentName;
            [MoonSharpHidden] set => _currentName = value;
        }

        public int ChoiceSelected
        {
            get => _choiceSelected;
            [MoonSharpHidden] set => _choiceSelected = value;
        }

        public bool GetFlag(string flag)
        {
            return _flags.Contains(flag);
        }

        public void SetFlag(string flag, bool set)
        {
            if (set)
            {
                _flags.Add(flag);
            }
            else
            {
                _flags.Remove(flag);
            }
        }
    }
}
