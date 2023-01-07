using System.Collections;
using UnityEngine;

namespace GameJam
{
    public class FoodSpawner : MonoBehaviour, IFoodSpawner
    {
        bool _isSpawningInProgress;
        Fruit _fruit;
        
        public bool CanSpawnFood()
        {
            return !_isSpawningInProgress && _fruit == null;
        }

        public bool SpawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            if(_fruit != null || _isSpawningInProgress) 
                return false;
         
            StartCoroutine(spawnFood(fruitPrefab, delaySeconds));
            return true;
        }
        
        public bool DropFood()
        {
            if(_fruit == null || _isSpawningInProgress) 
                return false;
            
            _fruit = null;
            return true;
        }
        
        IEnumerator spawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f)
        {
            _isSpawningInProgress = true;
            
            if(delaySeconds > 0)
                yield return new WaitForSeconds(delaySeconds);
            
            _fruit = Instantiate(fruitPrefab, transform.position, Quaternion.identity);
            _isSpawningInProgress = false;
        }
    }
}
