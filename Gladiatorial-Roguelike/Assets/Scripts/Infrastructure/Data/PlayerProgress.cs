using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Profile Profile;
        public CurrentRun CurrentRun;
        public PermaDeck PermaDeck;
        public PlayerProgress()
        {
            CurrentRun = new CurrentRun(); 
            Profile = new Profile();
            PermaDeck = new PermaDeck();
        }
        public void ClearProgress() => 
            CurrentRun = new CurrentRun();
    }
}