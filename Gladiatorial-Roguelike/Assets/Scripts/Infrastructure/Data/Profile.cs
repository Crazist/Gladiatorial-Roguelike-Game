using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class Profile
    {
        public PermaDeck PermaDeck;
        public event Action OnExpChanged;
        public event Action OnLevelChanged;

        private int _level;
        private int _exp;

        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnLevelChanged?.Invoke();
                }
            }
        }

        public int Exp
        {
            get => _exp;
            set
            {
                if (_exp != value)
                {
                    _exp = value;
                    OnExpChanged?.Invoke();
                }
            }
        }

        public Profile()
        {
            PermaDeck = new PermaDeck();
        }
    }
}