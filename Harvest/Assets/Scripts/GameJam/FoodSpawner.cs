using System.Collections;
using GameJam.System.Data;
using UnityEngine;

namespace GameJam
{
    public class FoodSpawner : MonoBehaviour, IFoodSpawner
    {
        [SerializeField] SpriteRenderer _editorImage;
        [SerializeField] float _spawnLocationOffsetX = 17;
        [SerializeField] float _spawnLocationOffsetY = 40;
        
        bool _isSpawningInProgress;
        
        void Awake()
        {
            _editorImage.enabled = false;
        }
        
        public bool CanSpawnFood()
        {
            return !_isSpawningInProgress;
        }

        public void SpawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            if(_isSpawningInProgress)
                return;
            
            StartCoroutine(spawnFood(fruitPrefab, delaySeconds));
        }
        
        IEnumerator spawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            _isSpawningInProgress = true;
            
            if(delaySeconds > 0)
                yield return new WaitForSeconds(delaySeconds);
            
            var offsetX = Random.Range(_spawnLocationOffsetX * -1, _spawnLocationOffsetX);
            var offsetY = Random.Range(_spawnLocationOffsetY * -1, _spawnLocationOffsetY);
            var spawnLocation = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);

            Instantiate(fruitPrefab, spawnLocation, Quaternion.identity, transform);
            GameDataStore.Instance.GainFruitInLevel();
            _isSpawningInProgress = false;
        }
    }
}
