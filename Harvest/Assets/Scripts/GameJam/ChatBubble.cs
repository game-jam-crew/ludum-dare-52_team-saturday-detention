using System.Collections;
using TMPro;
using UnityEngine;

namespace GameJam
{
    public class ChatBubble : MonoBehaviour
    {
        [SerializeField] TMP_Text _chatText;
        [SerializeField] float _defaultChatDurationSeconds = 5.0f;

        CanvasGroup _canvasGroup;

        public void DisplayChat(string message, float? durationInSeconds = null)
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 1.0f;

            _chatText.SetText(message);
            
            StartCoroutine(fadeOutAfter(durationInSeconds ?? _defaultChatDurationSeconds));
        }
        
        IEnumerator fadeOutAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            
            while(_canvasGroup.alpha > 0.01)
            {
                _canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
            
            Destroy(gameObject);            
        }
    }
}
