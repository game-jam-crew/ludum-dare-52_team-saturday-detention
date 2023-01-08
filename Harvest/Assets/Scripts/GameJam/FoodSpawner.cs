using System.Collections;
using GameJam.System.Data;
using UnityEngine;

namespace GameJam
{
    public class FoodSpawner : MonoBehaviour, IFoodSpawner
    {
        [SerializeField] float _spawnLocationOffset = 20;
        
        bool _isSpawningInProgress;
        
        public bool CanSpawnFood()
        {
            return !_isSpawningInProgress;
        }

        public void SpawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            if(_isSpawningInProgress)
                return;
            
            if(GameDataStore.Instance.IsFruitAtMax())
                return;
            
            StartCoroutine(spawnFood(fruitPrefab, delaySeconds));
        }
        
        IEnumerator spawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            _isSpawningInProgress = true;
            
            if(delaySeconds > 0)
                yield return new WaitForSeconds(delaySeconds);
            
            var offsetX = Random.Range(_spawnLocationOffset * -1, _spawnLocationOffset);
            var offsetY = Random.Range(_spawnLocationOffset * -1, _spawnLocationOffset);
            var spawnLocation = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);

            Instantiate(fruitPrefab, spawnLocation, Quaternion.identity, transform);
            GameDataStore.Instance.GainFruitInLevel();
            _isSpawningInProgress = false;
        }
    }
}
