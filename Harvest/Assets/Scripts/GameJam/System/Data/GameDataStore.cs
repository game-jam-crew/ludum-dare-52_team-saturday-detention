using GameJam.System.Events;

namespace GameJam.System.Data
{
    public class GameDataStore : SingletonMonoBehaviour<GameDataStore>
    {
        int _highScore;
        int _currentScore;
        
        public int HighScore => _highScore;
        public int CurrentScore => _currentScore;
        
        public void RefreshDataForNewGame()
        {
            _currentScore = 0;
        }
        
        public void GainPoints(int points)
        {
            _currentScore += points;
            GameEventManager.Instance.RaiseEvent("ScorePointsGained");

            if(_currentScore > _highScore)
                _highScore = _currentScore;
        }
    }
}
