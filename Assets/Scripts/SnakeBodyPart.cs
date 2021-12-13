using UnityEngine;

public class SnakeBodyPart
{
    private SnakeMovePosition position;
    private Transform transform;
    public SnakeBodyPart(int bodyIndex)
    {
        GameObject snakeBodyPart = new GameObject();
        SpriteRenderer sr = snakeBodyPart.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeBodySprite;
        sr.sortingOrder = -bodyIndex;
        transform = snakeBodyPart.transform;
    }

    public Vector2Int GetPosition() {
        return position.GetGridPosition();
    }

    public void SetSnakeMovePosition(SnakeMovePosition position)
    {
        this.position = position;
        transform.position = new Vector3(position.GetGridPosition().x, position.GetGridPosition().y);
        float angle;
        switch (position.GetDirection())
        {
            default:
            case Snake.Direction.Up:
                switch (position.GetPreviousDirection())
                {
                    default:
                        angle = 0; break;
                    case Snake.Direction.Left: angle = 45; 
                    transform.position += new Vector3(.2f,.2f);
                    break;
                    case Snake.Direction.Right: angle = -45;
                    transform.position += new Vector3(-.2f,.2f);
                    break;
                }
                break;
            case Snake.Direction.Down:
                switch (position.GetPreviousDirection())
                {
                    default:
                        angle = 180; break;
                    case Snake.Direction.Left: angle = 180-45;
                    transform.position += new Vector3(.2f,-.2f);
                    break;
                    case Snake.Direction.Right: angle = 180+45;
                    transform.position += new Vector3(-.2f,-.2f);
                    break;
                }
                break;
            case Snake.Direction.Left:
                switch (position.GetPreviousDirection())
                {
                    default:
                        angle = -90; break;
                    case Snake.Direction.Down: angle = -45; 
                    transform.position += new Vector3(-.2f,.2f);
                    break;
                    case Snake.Direction.Up: angle = 45; 
                    transform.position += new Vector3(-.2f,-.2f);
                    break;
                }
                break;
            case Snake.Direction.Right:
                switch (position.GetPreviousDirection())
                {
                    default:
                        angle = 90; break;
                    case Snake.Direction.Down: angle = 45; 
                    transform.position += new Vector3(.2f,.2f);
                    break;
                    case Snake.Direction.Up: angle = -45;
                    transform.position += new Vector3(.2f,-.2f);
                    break;
                }
                break;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}