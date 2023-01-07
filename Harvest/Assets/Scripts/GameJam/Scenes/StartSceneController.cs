using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scenes
{
    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] Button _startGameButton;
        
        void Awake()
        {
            // _startGameButton.onClick += startGameHandler;
        }
        
        void startGameHandler()
        {
        }
    }
}
