using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam.System.View
{
    public class GameViewManager : SingletonMonoBehaviour<GameViewManager>
    {
        [SerializeField] ChatBubble _chatBubblePrefab;
        
        public void OpenGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
        
        public ChatBubble ShowChatFor(GameObject chatSource, string message)
            => showChat(chatSource, message, null);
                
        public ChatBubble ShowChatFor(GameObject chatSource, string message, float displayDurationSeconds)
            => showChat(chatSource, message, displayDurationSeconds);
        
        ChatBubble showChat(GameObject chatSource, string message, float? displayDurationSeconds = null)
        {
            var chatBubble = Instantiate(_chatBubblePrefab, Vector2.zero, Quaternion.identity, chatSource.transform);
            chatBubble.DisplayChat(message, displayDurationSeconds);
            return chatBubble;
        }
    }
}
