using GameJam.System.Data;
using TMPro;
using UnityEngine;

namespace GameJam.UI
{
    public class StartGuiPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text _highScoreText;
        
        void Update()
        {
            _highScoreText.SetText(GameDataStore.Instance.HighScore.ToString());
        }
    }
}
