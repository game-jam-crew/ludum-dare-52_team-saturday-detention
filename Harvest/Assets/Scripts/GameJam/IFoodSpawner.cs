namespace GameJam
{
    public interface IFoodSpawner 
    {
        bool CanSpawnFood();
        void SpawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f);
    }
}