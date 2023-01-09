using UnityEngine;
using UnityEngine.SceneManagement;
using GameJam.System.Data;

namespace GameJam.System.View
{
    public class GameViewManager : SingletonMonoBehaviour<GameViewManager>
    {
       
        [SerializeField] GameObject _chatBubblePrefab;
       
        public void OpenGameScene()
        {
            GameDataStore.Instance.ResetGameData();
            SceneManager.LoadScene("GameScene");
        }
        
        public void OpenStartScene()
        {
            GameDataStore.Instance.ResetGameData();
            SceneManager.LoadScene("StartScene");
        }
        public GameObject ShowChatAt(Vector2 location, string message)
            => showChat(location, message, null);
                
        public GameObject ShowChatAt(Vector2 location, string message, float displayDurationSeconds)
            => showChat(location, message, displayDurationSeconds);
        
        GameObject showChat(Vector2 location, string message, float? displayDurationSeconds = null)
        {
            var chatBubbleGameObject = Instantiate(_chatBubblePrefab, location, Quaternion.identity);
            var chatBubble = chatBubbleGameObject.GetComponent<ChatBubble>();
            chatBubble.DisplayChat(message, displayDurationSeconds);
            return chatBubbleGameObject;
        }

    }
}
