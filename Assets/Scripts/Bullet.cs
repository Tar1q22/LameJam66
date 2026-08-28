using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 5;
    float worldHeight;
    void Start()
    {
        worldHeight = Camera.main.orthographicSize * 2;
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed*Time.deltaTime);

        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}