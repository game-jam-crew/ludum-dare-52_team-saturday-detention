using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameJam.System.View;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] bool _allowConversations;
        [SerializeField] Fruit _fruitPrefab;

        [SerializeField] float _moodRating = 1.0f;
        [SerializeField] SpriteRenderer _moodImageRenderer;
        [SerializeField] Sprite _happySprite;
        [SerializeField] Sprite _mehSprite;
        [SerializeField] Sprite _sadSprite;
        [SerializeField] Sprite _angrySprite;
        
        GameObject _chatBubble;
        
        List<IFoodSpawner> _foodSpawners = new List<IFoodSpawner>();
       
        void Start()
        {
            _foodSpawners = GetComponentsInChildren<IFoodSpawner>().ToList();
            StartCoroutine(coordinateFoodSpawning());
            
            _moodImageRenderer.sprite = _angrySprite;
        }
        
        void Update()
        {
            if(_allowConversations && _chatBubble == null)
                spawnChat();
            
            _moodImageRenderer.sprite = getMoodSprite();
        }
        
        IEnumerator coordinateFoodSpawning()
        {
            yield return new WaitForSeconds(0.1f);
            
            foreach(var spawner in _foodSpawners)
            {
                if(!spawner.CanSpawnFood())
                    continue;

                var randomSpawnDelay = Random.Range(0.0F, 5.0F);
                spawner.SpawnFood(_fruitPrefab, randomSpawnDelay);
            }
        }
        
        void spawnChat()
        {
            _chatBubble = GameViewManager.Instance.ShowChatFor(gameObject, "Hello Ian!");
        }
        
        Sprite getMoodSprite()
        {
            if(_moodRating >= 0.85f)
                return _happySprite;

            if(_moodRating >= 0.5f)
                return _mehSprite;

            if(_moodRating >= 0.1f)
                return _sadSprite;
            
            return _angrySprite;
        }
    }
}
