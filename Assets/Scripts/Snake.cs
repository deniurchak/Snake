using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    private bool moved;

    private enum State
    {
        Alive,
        Dead
    }

    private State state;
    private GameHandler gameHandler;
    private SnakeBodyPart snakeBody;
    private Vector2Int gridPosition;
    public Direction direction;
    private float gridMoveTimer;

    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    [SerializeField]
    private float gridMoveMaxTimer = 0.5f;

    private enum InputDirection
    {
        Right,
        Left,
        Down,
        Up
    };
    private InputDirection inputDirection;

    void Awake()
    {
        state = State.Alive;
        inputDirection = InputDirection.Right;
        gridPosition = new Vector2Int(0, 0);
        direction = Direction.Right;
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        gridMoveTimer = gridMoveMaxTimer;
        gameHandler = gameObject.GetComponentInParent<GameHandler>();
        snakeBodySize = 0;
    }

    void Start()
    {

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeHeadSprite;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if (state != State.Dead)
        {
            Direction direction = Turn();
            Move(direction);
        }
        if (state == State.Dead)
        {
            GameOver.ShowStatic();
        }
    }
    public List<Vector2Int> GetGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }
    private void Move(Direction newDirection)
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveMaxTimer)
        {
            direction = newDirection;
            gridMoveTimer -= gridMoveMaxTimer;

            SnakeMovePosition previousPosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousPosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(gridPosition, previousPosition, direction);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (direction)
            {
                default:
                case Direction.Right: gridMoveDirectionVector = Vector2Int.right; break;
                case Direction.Left: gridMoveDirectionVector = Vector2Int.left; break;
                case Direction.Up: gridMoveDirectionVector = Vector2Int.up; break;
                case Direction.Down: gridMoveDirectionVector = Vector2Int.down; break;
            }
            gridPosition += gridMoveDirectionVector;
            gridPosition = gameHandler.ValidateGridPosition(gridPosition);

            if (gameHandler.TrySnakeEatFood(gridPosition))
            {
                snakeBodySize++;
                CreateSnakeBodyPart();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }


            Vector3 rotation = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = rotation;

            UpdateSnakeBodyParts();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartPosition = snakeBodyPart.GetPosition();
                if (gridPosition == snakeBodyPartPosition)
                {
                    state = State.Dead;
                }
            }
        }
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count - 1));
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection = InputDirection.Right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection = InputDirection.Left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection = InputDirection.Down;
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection = InputDirection.Up;
        }
    }
    private Direction Turn()
    {
        Direction newDirection;
        if (inputDirection == InputDirection.Up)
        {
            if (direction != Direction.Down)
            {
                newDirection = Direction.Up;
                return newDirection;
            }
        }
        if (inputDirection == InputDirection.Down)
        {
            if (direction != Direction.Up)
            {
                newDirection = Direction.Down;
                return newDirection;
            }
        }
        if (inputDirection == InputDirection.Right)
        {
            if (direction != Direction.Left)
            {
                newDirection = Direction.Right;
                return newDirection;
            }
        }
        if (inputDirection == InputDirection.Left)
        {
            if (direction != Direction.Right)
            {
                newDirection = Direction.Left;
                return newDirection;
            }
        }
        return direction;
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
