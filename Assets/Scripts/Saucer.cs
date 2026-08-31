using Unity.VisualScripting;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    float speed = 5;
    Vector2 dir = Vector2.right;
    float targetY = 4f;
    BoxCollider2D bc;
    private float shootCooldown = 1f;
    private float nextShotTime;

    [SerializeField] GameObject laser;
    [SerializeField] GameObject gameLogic;
    GameLogic logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(-12, targetY);
        bc = GetComponent<BoxCollider2D>();
        logic = FindAnyObjectByType<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)transform.position + dir*speed*Time.deltaTime;
        // if (bc.IsTouchingLayers(6))
        // {
        //     Explode();
        // }
        if (dir == Vector2.right)
        {
            if (transform.position.x > 8)
            {
                targetY -= 1;
                dir = Vector2.down;
            }
        }
        if (dir == Vector2.left)
        {
            if (transform.position.x < -8)
            {
                targetY -= 1;
                dir = Vector2.down;
            }
        }
        if (dir == Vector2.down)
        {
            if (transform.position.y <= targetY)
            {
                if (transform.position.x > 0)
                {
                    dir = Vector2.left;
                }
                else
                {
                    dir = Vector2.right;
                }
            }
        }
        
        
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shootCooldown+ Random.Range(-0.3f, 0.3f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Explode(collision.gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(laser, transform.position, Quaternion.identity);
    }

    private void Explode(GameObject bullet)
    {
        GameLogic.Instance.AddScore(10);
        GameLogic.Instance.enemiesLeft--;

        Destroy(bullet);
        Destroy(gameObject);
    }
}
