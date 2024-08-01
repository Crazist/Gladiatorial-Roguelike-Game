using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class Profile
    {
        [field: NonSerialized] public event Action OnExpChanged;
        [field: NonSerialized] public event Action OnLevelChanged;

        [SerializeField] private int _level;
        [SerializeField] private int _exp;
        [SerializeField] private int _currency;

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

        public int Currency
        {
            get => _currency;
            set => _currency = value;
        }
    }
}