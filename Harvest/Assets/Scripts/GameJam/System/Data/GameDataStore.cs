using System.Collections;
using GameJam.System.Events;
using GameJam.System.View;
using UnityEngine;

namespace GameJam.System.Data
{
    public class GameDataStore : SingletonMonoBehaviour<GameDataStore>
    {
        const string HIGH_SCORE_KEY = "DATA_KEY::HIGH_SCORE";
        const int MAX_FRUIT_PER_LEVEL = 50;
        const float GAME_END_COUNT_SECONDS = 5;        
        
        static int _highScore;
        static int _currentScore;
        static int _fruitInLevel;
        static float _timeBeforeGameEnds;

        
        // TODO: consider how to "high score" the mood of the tree.
        public int HighScore => _highScore;
        public int CurrentScore => _currentScore;
        public float TimeLeftInGame => _timeBeforeGameEnds;
        
        void Start()
        {
            _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }
        
        public bool IsFruitAtMax()
        {
            return _fruitInLevel >= MAX_FRUIT_PER_LEVEL;
        }
        
        public void ResetGameData()
        {
            _timeBeforeGameEnds = 0;
            _currentScore = 0;
            _fruitInLevel = 0;
        }        
        
        public void GainPoints(int points)
        {
            _currentScore += points;
            GameEventManager.Instance.RaiseEvent("ScorePointsGained");

            if(_currentScore > _highScore)
                syncHighScore(_currentScore);
        }
        
        public void GainFruitInLevel()
        {
            _fruitInLevel += 1;
        }
        

        public void TriggerGameOverTimer()
        {
            if(_timeBeforeGameEnds <= 0)
                StartCoroutine(GameEndCountdownCoroutine());
        }

        IEnumerator GameEndCountdownCoroutine()
        {
            _timeBeforeGameEnds = GAME_END_COUNT_SECONDS;
            
            while(_timeBeforeGameEnds > 0)
            {
                yield return new WaitForSeconds(1);
                _timeBeforeGameEnds -= 1;
            }
            
            GameViewManager.Instance.OpenStartScene();
        }
        
        void syncHighScore(int score)
        {
            _highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        }
    }
}
