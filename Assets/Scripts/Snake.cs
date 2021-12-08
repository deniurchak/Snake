using UnityEngine;
using System.Collections.Generic;

//TODO: remove the logic that creates and destroys body parts indefinitely, make them move instead
public class Snake : MonoBehaviour
{
    private GameHandler gameHandler;
    private SnakeBodyPart snakeBody;
    private Vector2Int gridPosition;
    private Vector2Int direction;
    private float gridMoveTimer;

    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    [SerializeField]
    private float gridMoveMaxTimer = 0.5f;

    private bool turnRight;
    private bool turnLeft;
    private bool turnUp;
    private bool turnDown;

    public List<Vector2Int> GetGridPositionList()
    {
        return snakeMovePositionList;
    }

    void Awake()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeHeadSprite;

        gridPosition = new Vector2Int(0, 0);
        direction = new Vector2Int(1, 0);
        snakeMovePositionList = new List<Vector2Int>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        gridMoveTimer = gridMoveMaxTimer;
        gameHandler = gameObject.GetComponentInParent<GameHandler>();
        snakeBodySize = 0;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        Turn();
        Move();
    }
    private void Move()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveMaxTimer)
        {
            gridMoveTimer -= gridMoveMaxTimer;

            snakeMovePositionList.Insert(0, gridPosition);

            gridPosition += direction;

            if (gameHandler.TrySnakeEatFood(gridPosition))
            {
                snakeBodySize++;
                CreateSnakeBodyPart();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            Vector3 rotation = new Vector3(0, 0, GetAngleFromVector(direction) - 90);

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = rotation;

            UpdateSnakeBodyParts();
        }
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }

    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count - 1));
    }
    private void GetInput()
    {
        turnRight = Input.GetKey(KeyCode.D);
        turnLeft = Input.GetKey(KeyCode.A);
        turnUp = Input.GetKey(KeyCode.W);
        turnDown = Input.GetKey(KeyCode.S);
    }
    private void Turn()
    {
        if (turnUp)
        {
            if (direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
            }
        }
        if (turnDown)
        {
            if (direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
            }
        }
        if (turnRight)
        {
            if (direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
            }
        }
        if (turnLeft)
        {
            if (direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
