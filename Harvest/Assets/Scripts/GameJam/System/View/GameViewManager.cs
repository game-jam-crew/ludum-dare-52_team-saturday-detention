using UnityEngine.SceneManagement;

namespace GameJam.System.View
{
    public class GameViewManager : SingletonMonoBehaviour<GameViewManager>
    {
        public void OpenGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
