namespace GameJam
{
    public interface IFoodSpawner 
    {
        bool CanSpawnFood();
        bool SpawnFood(Fruit fruitPrefab, float delaySeconds = 0.0f);
        bool DropFood();
    }
}