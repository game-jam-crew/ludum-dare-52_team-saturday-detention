using GameJam.System.View;
using UnityEngine;

namespace GameJam
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] bool _allowConversations;
        
        GameObject _chatBubble;
        
        void Update()
        {
            if(_allowConversations && _chatBubble == null)
                spawnChat();
        }
        
        void spawnChat()
        {
            _chatBubble = GameViewManager.Instance.ShowChatFor(gameObject, "Hello Ian!");
        }
    }
}
