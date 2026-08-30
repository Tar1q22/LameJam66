using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    float speed = 5;
    float worldHeight;
    Vector2 dirToMouse;
    Vector2 screenBounds;
    void Start()
    {
        worldHeight = Camera.main.orthographicSize * 2;
        dirToMouse = ((Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)transform.position).normalized;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -10));
    }

    void Update()
    {
        
        transform.position = new Vector2(transform.position.x+(dirToMouse.x*speed*Time.deltaTime), transform.position.y + (dirToMouse.y*speed*Time.deltaTime));

        if (transform.position.y > 10 || transform.position.y < -10 || transform.position.x < -100 || transform.position.x > 100)
        {
            Destroy(gameObject);
        }
    }
}