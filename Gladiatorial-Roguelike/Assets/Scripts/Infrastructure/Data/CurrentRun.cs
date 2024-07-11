using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class CurrentRun
    {
        public event Action OnExpChanged;
        public event Action OnLevelChanged;

        private int _level;
        private float _exp;

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

        public float Exp
        {
            get => _exp;
            set
            {
                if (Math.Abs(_exp - value) > 0.01f)
                {
                    _exp = value;
                    OnExpChanged?.Invoke();
                }
            }
        }
    }
}