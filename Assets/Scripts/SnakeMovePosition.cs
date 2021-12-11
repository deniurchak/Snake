using UnityEngine;

public class SnakeMovePosition
{
    private Vector2Int gridPosition;
    private Snake.Direction direction;

    private SnakeMovePosition previousPosition;
    public SnakeMovePosition(Vector2Int gridPosition,SnakeMovePosition previousPosition, Snake.Direction direction) {
        this.direction = direction;
        this.gridPosition = gridPosition;
        this.previousPosition = previousPosition;
    }

    public Vector2Int GetGridPosition() {
        return gridPosition;
    }

    public Snake.Direction GetDirection() {
        return direction;
    }

    public Snake.Direction GetPreviousDirection() {
        return previousPosition == null ?  Snake.Direction.Right : previousPosition.direction;
    }
}