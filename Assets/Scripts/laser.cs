using UnityEngine;

public class laser : MonoBehaviour
{
    float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = (Vector2)transform.position+Vector2.down*speed*Time.deltaTime;
        if (transform.position.y < -12)
        {
            Destroy(gameObject);
        }
    }
}
