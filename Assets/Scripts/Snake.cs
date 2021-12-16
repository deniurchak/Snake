using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public enum Direction {
        Left,
        Right,
        Up,
        Down
    }

    private Direction newDirection;
    private bool moved;

    private enum State {
        Alive,
        Dead
    }

    private State state ;
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

    private bool turnRight;
    private bool turnLeft;
    private bool turnUp;
    private bool turnDown;


    void Awake()
    {
        state = State.Alive;
        gridPosition = new Vector2Int(0, 0);
        direction = Direction.Right; 
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        gridMoveTimer = gridMoveMaxTimer;
        gameHandler = gameObject.GetComponentInParent<GameHandler>();
        snakeBodySize = 0;
    }

    void Start() {

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = GameAssets.i.snakeHeadSprite;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if(state != State.Dead) {
            Turn();
            Move();
        }
        if(state == State.Dead) {
            GameOver.ShowStatic();
        }
    }
    public List<Vector2Int> GetGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }
    private void Move()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveMaxTimer)
        {
            moved = false;
            gridMoveTimer -= gridMoveMaxTimer;
            
            SnakeMovePosition previousPosition = null;
            if(snakeMovePositionList.Count >0 ) {
                previousPosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(gridPosition, previousPosition, direction);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch(direction) {
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

            foreach(SnakeBodyPart snakeBodyPart in snakeBodyPartList) {
                Vector2Int snakeBodyPartPosition = snakeBodyPart.GetPosition();
                if(gridPosition == snakeBodyPartPosition) {
                    state = State.Dead;
                }
            }
            moved = true;
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
        turnRight = Input.GetKey(KeyCode.D);
        turnLeft = Input.GetKey(KeyCode.A);
        turnUp = Input.GetKey(KeyCode.W);
        turnDown = Input.GetKey(KeyCode.S);
    }
    private void Turn()
    {   if(!moved) {
        return;
    }
        if (turnUp)
        {
            if (direction != Direction.Down)
            {
                newDirection= Direction.Up;
            }
        }
        if (turnDown)
        {
            if (direction != Direction.Up)
            {
                newDirection= Direction.Down;
            }
        }
        if (turnRight)
        {
            if (direction != Direction.Left)
            {
                newDirection= Direction.Right;
            }
        }
        if (turnLeft)
        {
            if (direction != Direction.Right)
            {
                newDirection= Direction.Left;
            }
        }
        if(moved) {
            direction = newDirection;
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
