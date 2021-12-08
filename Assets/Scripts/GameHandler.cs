using UnityEngine;
public class GameHandler : MonoBehaviour
{
    private LevelGrid levelGrid;
    private Snake snake;
    private Food food;
    private Vector2Int foodPosition;

    void Start()
    {
        levelGrid = new LevelGrid(10, 10);

        snake = GetComponentInChildren<Snake>();
        food = GetComponentInChildren<Food>();

        CreateFoodPosition();
        food.Respawn(foodPosition);
    }

    void CreateFoodPosition()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(-levelGrid.width, levelGrid.width),
            Random.Range(-levelGrid.height, levelGrid.height));
        } while (snake.GetGridPositionList().IndexOf(foodPosition) != -1);
    }

    public bool TrySnakeEatFood(Vector2Int snakePosition)
    {
        bool snakeAteFood = snakePosition == foodPosition;
        if (snakeAteFood)
        {
            CreateFoodPosition();
            food.Respawn(foodPosition);
        }
        return snakeAteFood;
    }
}