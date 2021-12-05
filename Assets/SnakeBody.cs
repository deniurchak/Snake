using UnityEngine;
public class SnakeBody : MonoBehaviour
{
    [SerializeField]
    private Texture2D tex;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private SpriteRenderer sr;


    [SerializeField]
    private Color color = Color.white;

    public void Create(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y);
    }

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = color;
    }

    void Start()
    {
        tex = new Texture2D(100, 100);
        sr.sprite = sprite ? sprite : Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
        new Vector2(0.5f, 0.5f), 100.0f);
    }
}