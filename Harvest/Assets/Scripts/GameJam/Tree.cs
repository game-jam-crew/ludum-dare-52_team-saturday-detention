using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameJam.System.Data;
using GameJam.System.View;
using UnityEngine;

namespace GameJam
{
    public class Tree : MonoBehaviour
    {
        const float MOOD_MAX_RATING = 1.0f;
        const float MOOD_HAPPY_RATING_THRESHOLD = 0.85f;
        const float MOOD_STANDARD_RATING_THRESHOLD = 0.5f;
        const float MOOD_POOR_RATING_THRESHOLD = 0.1f;        
        
        [SerializeField] Fruit _rottenFruitPrefab;
        [SerializeField] Fruit _poorFruitPrefab;
        [SerializeField] Fruit _standardFruitPrefab;
        [SerializeField] Fruit _rareFruitPrefab;

        [SerializeField] GameObject _chatBubbleSpawnLocation;
        
        [SerializeField] SpriteRenderer _moodImageRenderer;
        [SerializeField] Sprite _happySprite;
        [SerializeField] Sprite _mehSprite;
        [SerializeField] Sprite _sadSprite;
        [SerializeField] Sprite _angrySprite;
        
        [SerializeField] float _moodRating = 1.0f;
        float _moodRatingLoss = -0.15f;
        float _moodRatingGainFromAirCatch = 0.15f;
        float _moodRatingGainFromRotten = 0.01f;
        
        float _moodRegenTickInSeconds = 1.0f;
        float _moodRegenAmount = 0.01f;
        
        List<IFoodSpawner> _foodSpawners = new List<IFoodSpawner>();
       
        void Start()
        {
            _foodSpawners = GetComponentsInChildren<IFoodSpawner>().ToList();
            StartCoroutine(spawnFood());
            StartCoroutine(regenMood());
            
            _moodImageRenderer.sprite = _angrySprite;
        }
        
        void Update()
        {
            _moodImageRenderer.sprite = getMoodSprite();
        }
        
        int _lastOnTreeMessage = 0;
        public void FruitTakenOnTree()
        {
            var messages = new [] 
            { 
                "Wait until they fall!",
                "Not On The Tree!", 
                "OUCH! Stop it!",
                "I thought we were friends!",
                "It hurts! It HURTS SO BAD!",
            };

            showChat(messages[_lastOnTreeMessage], 0.5f);
            adjustMoodRating(_moodRatingLoss);
                        
            _lastOnTreeMessage += 1;
            if(_lastOnTreeMessage >= messages.Length - 1)
                _lastOnTreeMessage = 0;
        }
        
        public void FruitTakenInAir()
        {
            var messages = new [] 
            { 
                "Nice Catch!", 
                "You light up my day",
                "Fantastic!",
                "I'm glad we are friends.",
                "You're the coolest player around!",
            };
            var index = Random.Range(0, messages.Length);
            showChat(messages[index], 0.5f);
            adjustMoodRating(_moodRatingGainFromAirCatch);
        }
        
        public void FruitTakenOnGround() {}
        
        public void FruitTakenRotten()
        {
            adjustMoodRating(_moodRatingGainFromRotten);
        }
        
        public void FruitDropped()
        {
            if(GameDataStore.Instance.IsFruitAtMax())
            {
                GameDataStore.Instance.TriggerGameOverTimer();
                return;
            }

            StartCoroutine(spawnFood());
        }
        
        void showChat(string message, float duration)
        {
            var position = _chatBubbleSpawnLocation.transform.position;
            var x = position.x + Random.Range(-20, 20);
            var y = position.y + Random.Range(-20, 20);
            var location = new Vector2(x, y); 
            GameViewManager.Instance.ShowChatAt(location, message, duration);
        }
        
        IEnumerator regenMood()
        {
            while(true)
            {
                yield return new WaitForSeconds(_moodRegenTickInSeconds);
                adjustMoodRating(_moodRegenAmount);
            }
        }
        
        IEnumerator spawnFood()
        {
            yield return new WaitForSeconds(0.1f);
            
            foreach(var spawner in _foodSpawners)
            {
                if(!spawner.CanSpawnFood())
                    continue;

                var randomSpawnDelay = Random.Range(0.0F, 5.0F);
                spawner.SpawnFood(fruitToDrop(), randomSpawnDelay);
            }
        }
        
        void adjustMoodRating(float amount)
        {
            _moodRating += amount;
            
            if(_moodRating < 0) _moodRating = 0;
            if(_moodRating > MOOD_MAX_RATING) _moodRating = MOOD_MAX_RATING;
        }
        
        Sprite getMoodSprite()
        {
            if(_moodRating >= MOOD_HAPPY_RATING_THRESHOLD)
                return _happySprite;

            if(_moodRating >= MOOD_STANDARD_RATING_THRESHOLD)
                return _mehSprite;

            if(_moodRating >= MOOD_POOR_RATING_THRESHOLD)
                return _sadSprite;
            
            return _angrySprite;
        }
        
        Fruit fruitToDrop()
        {
            var rando = Random.Range(0.0f, 1.0f);
            
            if(_moodRating >= MOOD_HAPPY_RATING_THRESHOLD)
                return rando >= 0.70 ? _rareFruitPrefab : _standardFruitPrefab;

            if(_moodRating >= MOOD_STANDARD_RATING_THRESHOLD)
                return rando >= 0.20 ? _standardFruitPrefab : _poorFruitPrefab;

            if(_moodRating >= MOOD_POOR_RATING_THRESHOLD)
                return rando >= 0.20 ? _poorFruitPrefab : _rottenFruitPrefab;
            
            return _rottenFruitPrefab;
        }
    }
}
