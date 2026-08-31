using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 3;
    [SerializeField]Canvas gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D laser)
    {
        if (laser.gameObject.layer == 7)
        {
            health--;
            print("health");
            Destroy(laser.gameObject);
        }
        if (health <= 0)
        {
            print("game over");
            Time.timeScale = 0;
            gameOverScreen.enabled = true;
        }
    }
}
