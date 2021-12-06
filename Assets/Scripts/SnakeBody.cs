using UnityEngine;
using System.Collections.Generic;

public class SnakeBody 
{

    public SnakeBody(float timerToDestroy) {
        this.timerToDestroy = timerToDestroy;
    }

    [SerializeField]
    private Texture2D tex;
    [SerializeField]
    private Sprite sprite = GameAssets.i.snakeBodySprite;
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Color color = Color.white;

    private float timerToDestroy;
    private List<GameObject> snakeBodyParts; 

    public void Create(Vector2Int position)
    {
        GameObject snakeBodyPart = new GameObject();
        snakeBodyPart.transform.position = new Vector3(position.x, position.y);

        sr = snakeBodyPart.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = color;
        tex = new Texture2D(100, 100);
        sr.sprite = sprite ? sprite : Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
        new Vector2(0.5f, 0.5f), 100.0f);
        Object.Destroy(snakeBodyPart, this.timerToDestroy);
    }
}