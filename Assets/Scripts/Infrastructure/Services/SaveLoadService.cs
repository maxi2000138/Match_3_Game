using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService
    {
        private const string _levelNumberKey = "LVLNUMKEY";
    
        public void SaveLevelNumber(int lvlNumber)
        {
            PlayerPrefs.SetInt(_levelNumberKey, lvlNumber);
        }

        public int? LoadLevelNumber()
        {
            if(PlayerPrefs.HasKey(_levelNumberKey))
                return PlayerPrefs.GetInt(_levelNumberKey);

            return null;
        }
    }
}
