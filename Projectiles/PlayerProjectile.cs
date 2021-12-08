using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Other variables
    private GameManager gameManager;
    private Vector3 bulletDir;
    private const int bulletSpeed = 25;
    [SerializeField] private GameObject explosion;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SetBulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy of bullet is out of bounds
        if (transform.position.y > gameManager.vertBound + 2 || transform.position.y < -gameManager.vertBound - 2 ||
            transform.position.x > gameManager.horBound + 3 ||
            transform.position.x < -gameManager.horBound - 3)
        {
            Destroy(gameObject);
        }

        // Translate bullet in the direction of bulletDir
        transform.Translate(bulletDir * (Time.deltaTime * bulletSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy Enemy if Bullet Touches it.
        if (!other.gameObject.CompareTag("Enemy")) return;
        Destroy(gameObject);
        Destroy(other.gameObject);
        gameManager.UpdateScore();

        // Play Explosion Animation
        var enemyTransform = other.transform;
        Instantiate(explosion, enemyTransform.position + new Vector3(0, -0.4f, 0), enemyTransform.rotation);
    }

    private void SetBulletDirection()
    {
        switch (gameManager.bulletDir % 4)
        {
            case 0:
                bulletDir = Vector3.up;
                break;
            case 1:
                bulletDir = Vector3.right;
                break;
            case 2:
                bulletDir = Vector3.down;
                break;
            case 3:
                bulletDir = Vector3.left;
                break;
        }
    }
}