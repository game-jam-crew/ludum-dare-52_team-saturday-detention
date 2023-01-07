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
        
        ChatBubble _chatBubble;
        
        List<IFoodSpawner> _foodSpawners = new List<IFoodSpawner>();
       
        void Start()
        {
            _foodSpawners = GetComponentsInChildren<IFoodSpawner>().ToList();
            testFoodSpawners();
        }
        
        void Update()
        {
            if(_allowConversations && _chatBubble == null)
                spawnChat();
        }
        
        void spawnChat()
        {
            _chatBubble = GameViewManager.Instance.ShowChatFor(gameObject, "Hello Ian!");
        }
        
        void testFoodSpawners()
        {
            foreach(var spawner in _foodSpawners)
            {
                spawner.SpawnFood(_fruitPrefab);
            }
        }
    }
}
