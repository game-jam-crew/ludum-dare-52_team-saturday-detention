using GameJam.System.Events;
using UnityEngine;

namespace GameJam.System.Data
{
    public class GameDataStore : SingletonMonoBehaviour<GameDataStore>
    {
        const string HIGH_SCORE_KEY = "DATA_KEY::HIGH_SCORE";
        
        static int _highScore;
        static int _currentScore;
        
        public int HighScore => _highScore;
        public int CurrentScore => _currentScore;
        
        void Start()
        {
            _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }
        
        public void RefreshDataForNewGame()
        {
            _currentScore = 0;
        }
        
        public void GainPoints(int points)
        {
            _currentScore += points;
            GameEventManager.Instance.RaiseEvent("ScorePointsGained");

            if(_currentScore > _highScore)
                syncHighScore(_currentScore);
        }
        
        void syncHighScore(int score)
        {
            _highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        }
    }
}
