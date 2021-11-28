using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2Int gridPosition;
    private Vector2Int direction;
    private float gridMoveTimer;

    [SerializeField]
    private float gridMoveMaxTimer = 1f;

    private bool turnRight;
    private bool turnLeft;
    private bool turnUp;
    private bool turnDown;


    void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
        direction = new Vector2Int(1, 0);
        gridMoveTimer = gridMoveMaxTimer;
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

    private void Move() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveMaxTimer)
        {
            gridPosition += direction;
            gridMoveTimer -= gridMoveMaxTimer;

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction) - 90);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
