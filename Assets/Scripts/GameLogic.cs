using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject saucer;
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] PlayerHealth playerHealth;
    
    float currentTime;
    float timeAtLastWave = 0;
    
    public int enemiesLeft = 0;
    public static GameLogic Instance;

    [SerializeField] private TextMeshProUGUI finalScoreText;
    public int score { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameOverScreen != null)
            gameOverScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (currentTime - timeAtLastWave > 3f && enemiesLeft <= 0)
        {
            StartCoroutine(SpawnWave(10));
        }

    }

    private System.Collections.IEnumerator SpawnWave(int size)
    {
        enemiesLeft = size;
        for (int i = 0; i<size; i++)
        {
            Instantiate(saucer);
            yield return new WaitForSeconds(1);
        }
        timeAtLastWave = Time.time;
    }

    public void OnButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void AddScore(int amount)
    {
        score += amount;

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;
    }

    public void ShowGameOver()
    {
        if (gameOverScreen != null)
            gameOverScreen.enabled = true;

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;
    }
}
