using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameJam.System.View;
using UnityEngine;

namespace GameJam
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] Fruit _fruitPrefab;

        [SerializeField] float _moodRating = 1.0f;
        [SerializeField] SpriteRenderer _moodImageRenderer;
        [SerializeField] Sprite _happySprite;
        [SerializeField] Sprite _mehSprite;
        [SerializeField] Sprite _sadSprite;
        [SerializeField] Sprite _angrySprite;
        
        List<IFoodSpawner> _foodSpawners = new List<IFoodSpawner>();
       
        void Start()
        {
            _foodSpawners = GetComponentsInChildren<IFoodSpawner>().ToList();
            StartCoroutine(spawnFood());
            
            _moodImageRenderer.sprite = _angrySprite;
        }
        
        void Update()
        {
            _moodImageRenderer.sprite = getMoodSprite();
        }
        
        public void FruitTakenOnTree()
        {
            GameViewManager.Instance.ShowChatFor(gameObject, "OUCH!", 1.0f);
            _moodRating -= 0.25f;
        }
        
        public void FruitTakenInAir()
        {
            Debug.Log("The tree thinks you are amazing!");
        }
        
        public void FruitTakenOnGround()
        {
            Debug.Log("The tree is unimpressed!");
        }
        
        public void FruitTakenRotten()
        {
            Debug.Log("The tree laughs at you!");
        }
        
        public void FruitDropped()
        {
            StartCoroutine(spawnFood());
        }
        
        IEnumerator spawnFood()
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
