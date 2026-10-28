using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("السكربتات والمرتبطة")]
    public CyberRunner player;
    public ObstacleSpawner spawner;
    public CoinSpawner coinSpawner;
    public Transform ground;
    public Transform ceiling;

    [Header("إعدادات المستويات")]
    public float timePerLevel = 20f;
    private float timer = 0f;
    public int currentLevel = 1;

    [Header("نظام النقاط والعملات")]
    public TextMeshProUGUI scoreText;
    private float score = 0f;
    public TextMeshProUGUI coinsText;
    private int coinsCount = 0;

    [Header("شاشة الخسارة")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalCoinsText;
    public TextMeshProUGUI bestScoreText;
    private bool isGameOver = false;

    [Header("المؤثرات الصوتية (Audio)")]
    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip gameOverSound;
    public AudioClip flipSound;

    void Update()
    {
        if (isGameOver) return;

        score += Time.deltaTime * (player.runSpeed / 2f);
        if (scoreText != null) scoreText.text = "SCORE: " + Mathf.FloorToInt(score).ToString();

        timer += Time.deltaTime;
        if (timer >= timePerLevel)
        {
            LevelUp();
            timer = 0f;
        }
    }

    public void AddCoin()
    {
        coinsCount++;
        if (coinsText != null) coinsText.text = "COINS: " + coinsCount.ToString();

        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }

    public void PlayFlipSound()
    {
        if (audioSource != null && flipSound != null)
        {
            audioSource.PlayOneShot(flipSound);
        }
    }

    void LevelUp()
    {
        currentLevel++;
        player.runSpeed += 3f;

        if (spawner.spawnRate > 0.6f) spawner.spawnRate -= 0.2f;
        if (coinSpawner != null && coinSpawner.spawnRate > 0.5f) coinSpawner.spawnRate -= 0.15f;

        if (ground.position.y < -1.5f)
        {
            ground.position = new Vector3(ground.position.x, ground.position.y + 0.3f, ground.position.z);
            ceiling.position = new Vector3(ceiling.position.x, ceiling.position.y - 0.3f, ceiling.position.z);
            spawner.groundY += 0.3f;
            spawner.ceilingY -= 0.3f;
            if (coinSpawner != null) { coinSpawner.minY += 0.3f; coinSpawner.maxY -= 0.3f; }
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // إيقاف الزمن عند الخسارة
        gameOverPanel.SetActive(true);

        MusicController musicController = FindObjectOfType<MusicController>();
        if (musicController != null)
        {
            musicController.StopMusicOnDeath();
        }

        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        int finalScoreInt = Mathf.FloorToInt(score);
        if (finalScoreText != null) finalScoreText.text = "SCORE: " + finalScoreInt.ToString();
        if (finalCoinsText != null) finalCoinsText.text = "COINS: " + coinsCount.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (finalScoreInt > bestScore)
        {
            bestScore = finalScoreInt;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        if (bestScoreText != null) bestScoreText.text = bestScore.ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // إرجاع الزمن لطبيعته
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // الدالة الخاصة بالعودة للقائمة الرئيسية
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // إرجاع الزمن لطبيعته لتجنب تجميد اللعبة في المنيو
        SceneManager.LoadScene("MainMenu");
    }
}