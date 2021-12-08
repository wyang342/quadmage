using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI variables
    [SerializeField] private TextMeshProUGUI coinsTextInGame;
    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score;
    [SerializeField] private GameObject difficultyScreen;
    [SerializeField] private GameObject titleScreen;

    [SerializeField] private GameObject gameOverScreen;

    // Difficulty Screen Text
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinsTextDifficultyScreen;

    // Difficulty variables
    public float enemySpeed;
    public float enemyProjectileSpeed;
    public float enemy1FireRate;

    public int bulletDir;
    public bool allowFire = true;
    public bool isGameActive;
    public int waveNumber = 1;
    private GameObject[] enemies;
    private GameObject[] toRemove;

    // Player variables
    [SerializeField] private GameObject player;
    private SpriteRenderer playerSpriteRenderer;
    private BoxCollider2D playerBoxCollider;
    private Player playerSaveData;

    // Powerup booleans
    public bool hasFreeze;
    public bool hasDuo;
    public bool hasGhost;

    // Bound variables to limit player movement and spawn enemies and items based on camera dimensions
    [SerializeField] private Camera mainCam;
    public float horBound;
    public float vertBound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Setting horizontal and vertical movement & spawn bounds
        coinsTextInGame.text = "";
        var orthographicSize = mainCam.orthographicSize;
        vertBound = orthographicSize - 1;
        horBound = orthographicSize * mainCam.aspect - 1;
        
        // Getting necessary components
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerBoxCollider = player.GetComponent<BoxCollider2D>();
        playerSaveData = player.GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateScore()
    {
        // Update coins
        playerSaveData.coins++;
        coinsTextInGame.text = "Total: " + playerSaveData.coins;

        // Update current score
        score++;
        scoreText.text = "Score: " + score;

        // Updates high score (just #, not UI)
        if (score > playerSaveData.highScore)
        {
            playerSaveData.highScore = score;
        }
    }

    public void ToGameOverScreen()
    {
        // Saves game whenever you die
        playerSaveData.SavePlayer();
        
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        // Updating coins and score text
        coinsTextInGame.text = "Total: " + playerSaveData.coins;
        scoreText.text = "Score: " + score;
        
        isGameActive = true;
        allowFire = true;
        player.SetActive(true);
    }

    public void ToDifficultyScreen()
    {
        isGameActive = false;
        // Removes any powerups and changes player sprite to normal
        hasFreeze = false;
        hasDuo = false;
        hasGhost = false;
        playerSpriteRenderer.color = Color.white;

        // Destroy enemies and powerups from scene
        toRemove = GameObject.FindGameObjectsWithTag("ToRemove");
        foreach (GameObject o in toRemove)
        {
            GameObject parentPowerup = o.transform.parent.gameObject;
            Destroy(parentPowerup);
        }

        // Show and hide UI gameobjects
        difficultyScreen.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        coinsTextInGame.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        // Update text on difficulty screen
        highScoreText.text = "High Score: " + playerSaveData.highScore;
        coinsTextDifficultyScreen.text = "Total Enemies Killed: " + playerSaveData.coins;

        // Reset values
        score = 0;
        waveNumber = 1;
        bulletDir = 0;
    }

    public void CollisionWithPowerup(GameObject powerup)
    {
        audioSource.Play();   
        if (powerup.CompareTag("Freeze"))
        {
            hasDuo = false;
            hasFreeze = true;
            hasGhost = false;
            StartCoroutine(FreezeRoutine());
        }
        else if (powerup.CompareTag("Duo"))
        {
            hasDuo = true;
            hasFreeze = false;
            hasGhost = false;
            StartCoroutine(DuoRoutine());
        }
        else if (powerup.CompareTag("Ghost"))
        {
            hasDuo = false;
            hasFreeze = false;
            hasGhost = true;
            StartCoroutine(GhostRoutine());
        }
    }

    // Powerup Routines
    private IEnumerator FreezeRoutine()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.blue;
        }

        yield return new WaitForSeconds(7);
        hasFreeze = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator DuoRoutine()
    {
        playerSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(7);
        hasDuo = false;
        playerSpriteRenderer.color = Color.white;
    }

    private IEnumerator GhostRoutine()
    {
        playerSpriteRenderer.color = new Color(0, 0, 0);
        playerBoxCollider.isTrigger = true;
        yield return new WaitForSeconds(6);
        playerBoxCollider.isTrigger = false;
        hasGhost = false;
        playerSpriteRenderer.color = Color.white;
    }
}