using UnityEngine;

namespace GameJam
{
    public class FoodSpawner : MonoBehaviour, IFoodSpawner
    {
        Fruit _fruit;
        
        public bool HasFood()
        {
            return _fruit != null;
        }

        public bool SpawnFood(Fruit fruitPrefab)
        {
            if(_fruit != null) 
                return false;
         
            _fruit = Instantiate(fruitPrefab, transform.position, Quaternion.identity);
            return true;
        }
        
        public bool DropFood()
        {
            if(_fruit == null) 
                return false;
            
            _fruit = null;
            return true;
        }
    }
}
