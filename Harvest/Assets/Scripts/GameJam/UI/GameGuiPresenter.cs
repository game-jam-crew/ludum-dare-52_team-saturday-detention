using GameJam.System.Data;
using TMPro;
using UnityEngine;

namespace GameJam.UI
{
    public class GameCanvasPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text _currentScoreText;
        
        void Start()
        {
            PresenterRender();
        }
        
        public void PresenterRender()
        {
            _currentScoreText.SetText(GameDataStore.Instance.CurrentScore.ToString());
        }
    }
}
