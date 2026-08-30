using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject saucer;
    float currentTime;
    float timeAtLastWave = 0;
    
    bool spawning = false;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (currentTime - timeAtLastWave > 5.5f && spawning == false)
        {
            StartCoroutine(SpawnWave());
        }

    }

    private System.Collections.IEnumerator SpawnWave()
    {
        spawning = true;
        for (int i = 0; i<10; i++)
        {
            Instantiate(saucer);
            yield return new WaitForSeconds(1);
        }
        spawning = false;
        timeAtLastWave = Time.time;
    }
}
