using UnityEngine;

public class SnakeBodyPart 
{
    private Vector2Int gridPosition;
    private Transform transform;
    public SnakeBodyPart(int bodyIndex)
    {
        GameObject snakeBodyPart = new GameObject();
        SpriteRenderer sr = snakeBodyPart.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeBodySprite;
        sr.sortingOrder = -bodyIndex;
        transform = snakeBodyPart.transform;
    }

    public void SetGridPosition(Vector2Int position) {
        this.gridPosition = position;
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }
}