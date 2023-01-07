namespace GameJam
{
    public interface IFoodSpawner 
    {
        bool HasFood();
        bool SpawnFood(Fruit fruitPrefab);
        bool DropFood();
    }
}