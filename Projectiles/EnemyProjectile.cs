using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector2 bulletDir;
    private GameObject player;
    private Rigidbody2D projectileRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        
        LaunchProjectile();
    }

    private void FixedUpdate()
    {
        // Destroy of bullet is out of bounds
        if (transform.position.y > gameManager.vertBound + 2 || transform.position.y < -gameManager.vertBound - 2 ||
            transform.position.x > gameManager.horBound + 3 ||
            transform.position.x < -gameManager.horBound - 3)
        {
            Destroy(gameObject);
        }
    }

    // Deactivate player on trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || gameManager.hasGhost) return;
        other.gameObject.SetActive(false);
        Destroy(gameObject);
        gameManager.ToGameOverScreen();
    }

    private void LaunchProjectile()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        Vector3 playerPosition = player.transform.position;
        Vector3 position = transform.position;
        bulletDir = new Vector2(playerPosition.x - position.x,
            playerPosition.y - position.y).normalized;
        projectileRb.AddForce(bulletDir * gameManager.enemyProjectileSpeed);
    }
}