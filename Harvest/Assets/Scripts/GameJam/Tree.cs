using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameJam.System.View;
using UnityEngine;

namespace GameJam
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] bool _allowConversations;
        [SerializeField] Fruit _fruitPrefab;
        
        GameObject _chatBubble;
        
        List<IFoodSpawner> _foodSpawners = new List<IFoodSpawner>();
       
        void Start()
        {
            _foodSpawners = GetComponentsInChildren<IFoodSpawner>().ToList();
            StartCoroutine(coordinateFoodSpawning());
        }
        
        void Update()
        {
            if(_allowConversations && _chatBubble == null)
                spawnChat();
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
    }
}
