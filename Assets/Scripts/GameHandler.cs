using UnityEngine;
public class GameHandler : MonoBehaviour
{
    private LevelGrid levelGrid;
    private Score score;
    private Snake snake;
    private Food food;
    private Vector2Int foodPosition;

    void Start()
    {
        levelGrid = new LevelGrid(10, 10);
        score = GetComponentInChildren<Score>();
        snake = GetComponentInChildren<Snake>();
        food = GetComponentInChildren<Food>();
        CreateFoodPosition();
        food.Respawn(foodPosition);
    }

    void CreateFoodPosition()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(-levelGrid.width + 1, levelGrid.width - 1),
            Random.Range(-levelGrid.height + 1, levelGrid.height - 1));
        } while (snake.GetGridPositionList().IndexOf(foodPosition) != -1);
    }

    public bool TrySnakeEatFood(Vector2Int snakePosition)
    {
        bool snakeAteFood = snakePosition == foodPosition;
        if (snakeAteFood)
        {
            score.incrementScore();
            CreateFoodPosition();
            food.Respawn(foodPosition);
        }
        return snakeAteFood;
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x < -levelGrid.width + 1)
        {
            gridPosition.x = levelGrid.width - 1;
        }
        if (gridPosition.x > levelGrid.width - 1)
        {
            gridPosition.x = -levelGrid.width + 1;
        }
        if (gridPosition.y > levelGrid.height - 1)
        {
            gridPosition.y = -levelGrid.height + 1;
        }
        if (gridPosition.y < -levelGrid.height + 1)
        {
            gridPosition.y = levelGrid.height - 1;
        }
        return gridPosition;
    }
}