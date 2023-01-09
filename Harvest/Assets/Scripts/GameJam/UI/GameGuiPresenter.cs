using GameJam.System.Data;
using TMPro;
using UnityEngine;

namespace GameJam.UI
{
    public class GameCanvasPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text _currentScoreText;
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TMP_Text _gameOverIn;
        
        bool _renderGameOver = false;
        
        void Start()
        {
            _gameOverPanel.SetActive(false);
            PresenterRender();
        }
        
        void Update()
        {
            var gameOverInSeconds = GameDataStore.Instance.TimeLeftInGame;
            if(!_renderGameOver && gameOverInSeconds > 0)
            {
                _renderGameOver = true;
                _gameOverPanel.SetActive(true);
            }
                
            if(_renderGameOver)
                gameOverRender(gameOverInSeconds);
        }
        
        public void PresenterRender()
        {
            _currentScoreText.SetText(GameDataStore.Instance.CurrentScore.ToString());
        }
        
        void gameOverRender(float gameOverInSeconds)
        {
            _gameOverIn.SetText(gameOverInSeconds.ToString("f0"));
        }
    }
}
