using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy0;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject powerup0;
    [SerializeField] private GameObject powerup1;
    [SerializeField] private GameObject powerup2;

    private AudioSource audioSource;

    // bounds for spawn range
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn new wave if enemies == 0
        if (!gameManager.isGameActive || GameObject.FindGameObjectsWithTag("Enemy").Length != 0) return;
        SpawnEnemies(gameManager.waveNumber);
        gameManager.waveNumber++;
        if (gameManager.waveNumber % 5 == 0)
        {
            SpawnPowerup();
        }
    }

    void SpawnEnemies(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            int randomInt = Random.Range(0, 2);
            if (randomInt == 0)
            {
                GameObject newEnemy0 = Instantiate(enemy0,
                    new Vector3(Random.Range(-gameManager.horBound, gameManager.horBound),
                        Random.Range(-gameManager.vertBound, gameManager.vertBound), -1),
                    enemy0.transform.rotation);
                if (gameManager.hasFreeze)
                {
                    newEnemy0.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            else
            {
                GameObject newEnemy1 = Instantiate(enemy1,
                    new Vector3(Random.Range(-gameManager.horBound, gameManager.horBound),
                        Random.Range(-gameManager.vertBound, gameManager.vertBound), -1),
                    enemy1.transform.rotation);
                if (gameManager.hasFreeze)
                {
                    newEnemy1.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
        audioSource.Play();
    }

    void SpawnPowerup()
    {
        int randomInt = Random.Range(0, 3);

        switch (randomInt)
        {
            case 0:
                Instantiate(powerup0,
                    new Vector3(Random.Range(-gameManager.horBound, gameManager.horBound),
                        Random.Range(-gameManager.vertBound, gameManager.vertBound), -1),
                    powerup0.transform.rotation);
                break;
            case 1:
                Instantiate(powerup1,
                    new Vector3(Random.Range(-gameManager.horBound, gameManager.horBound),
                        Random.Range(-gameManager.vertBound, gameManager.vertBound), -1),
                    powerup1.transform.rotation);
                break;
            case 2:
                Instantiate(powerup2,
                    new Vector3(Random.Range(-gameManager.horBound, gameManager.horBound),
                        Random.Range(-gameManager.vertBound, gameManager.vertBound), -1),
                    powerup2.transform.rotation);
                break;
        }
    }
}