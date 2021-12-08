using UnityEngine;

public class Powerup : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        gameManager.CollisionWithPowerup(gameObject);
        Destroy(gameObject);
    }
}