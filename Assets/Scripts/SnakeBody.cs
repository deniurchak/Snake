using UnityEngine;

public class SnakeBody 
{

    public SnakeBody(float timerToDestroy) {
        this.timerToDestroy = timerToDestroy;
    }
    private float timerToDestroy;

    public void Create(Vector2Int position, Vector3 angles)
    {
        GameObject snakeBodyPart = new GameObject();
        snakeBodyPart.transform.position = new Vector3(position.x, position.y);
        snakeBodyPart.transform.eulerAngles = angles;

        SpriteRenderer sr = snakeBodyPart.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeBodySprite;

        Object.Destroy(snakeBodyPart, this.timerToDestroy);
    }
}