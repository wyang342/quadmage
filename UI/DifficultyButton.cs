using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject coinsText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] public float enemySpeed;
    [SerializeField] public float enemyProjectileSpeed;
    [SerializeField] public float enemy1FireRate;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowTextAndSetDifficulty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void ShowTextAndSetDifficulty()
    {
        titleScreen.SetActive(false);
        
        // Showing Text
        coinsText.SetActive(true);
        scoreText.SetActive(true);
        
        // Setting Difficulty
        gameManager.enemySpeed = enemySpeed;
        gameManager.enemyProjectileSpeed = enemyProjectileSpeed;
        gameManager.enemy1FireRate = enemy1FireRate;
        gameManager.StartGame();
    }
}