using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Awake()
    {
        transform.position = new Vector3(0,0);
    }
    public void Respawn(Vector2Int position)
    {
        gameObject.transform.position = new Vector3(position.x, position.y);
    }
}
