using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class CurrentRun
    {
        public Action<float> OnExpChange;
        public Action<int> OnLevelChange;
        
        private float _exp;
        private int _level;

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnLevelChange?.Invoke(_level);
            }
        }

        public float Exp
        {
            get => _exp;
            set
            {
                _exp = value;
                OnExpChange?.Invoke(_exp);
            }
        }
    }
}