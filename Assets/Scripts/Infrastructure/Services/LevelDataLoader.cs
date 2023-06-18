using System;
using StaticData;

namespace Infrastructure.Services
{
    public class LevelDataLoader
    {
        private StaticDataService _staticDataService;
        private int _lvlNumber = -1;

        public LevelInformationData LevelInformationData;
        private SaveLoadService _saveLoadService;

        public LevelDataLoader(StaticDataService staticDataService, SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void SaveCurrentLevel()
        {
            _saveLoadService.SaveLevelNumber(_lvlNumber);
        }

        public void SetRandomAnotherLevel()
        {
            Random rnd = new Random();
            int lvl = rnd.Next(1, _staticDataService.LevelsAmount+1);
        
            while(lvl == _lvlNumber)
                lvl = rnd.Next(1, _staticDataService.LevelsAmount+1);
        
            _lvlNumber = lvl;
        }

        public void LoadPreviousLevel()
        {
            LevelInformationData = _staticDataService.GetLevelData(_lvlNumber);
        }

        private void LoadRandomLevel()
        {
            Random rnd = new Random();
            _lvlNumber = rnd.Next(1, _staticDataService.LevelsAmount+1);
        }

        public void LoadLevel()
        {
            int? levelNumber = _saveLoadService.LoadLevelNumber();
        
            if (levelNumber != null)
                _lvlNumber = levelNumber.Value;
            else
                LoadRandomLevel();

            LevelInformationData = _staticDataService.GetLevelData(_lvlNumber);
        }
    }
}
