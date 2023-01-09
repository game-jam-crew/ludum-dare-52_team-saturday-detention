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
        public GameObject ShowChatFor(GameObject chatSource, string message)
            => showChat(chatSource, message, null);
                
        public GameObject ShowChatFor(GameObject chatSource, string message, float displayDurationSeconds)
            => showChat(chatSource, message, displayDurationSeconds);
        
        GameObject showChat(GameObject chatSource, string message, float? displayDurationSeconds = null)
        {
            var chatBubbleGameObject = Instantiate(_chatBubblePrefab, Vector2.zero, Quaternion.identity, chatSource.transform);
            var chatBubble = chatBubbleGameObject.GetComponent<ChatBubble>();
            chatBubble.DisplayChat(message, displayDurationSeconds);
            return chatBubbleGameObject;
        }

    }
}
