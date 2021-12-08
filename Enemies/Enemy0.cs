using System.Collections;
using UnityEngine;

public class Enemy0 : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    private GameManager gameManager;
    private bool canMove;

    private void Awake()
    {
        // Finding gameobjects
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // start color change and "can't damage me" coroutine
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        if (!gameManager.hasFreeze)
        {
            spriteRenderer.color = Color.grey;
        }

        enemyCollider.isTrigger = true;
        StartCoroutine(EnemySpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!gameManager.isGameActive || !canMove || gameManager.hasFreeze) return;
        Vector3 playerPosition = player.transform.position;
        Vector3 position = transform.position;
        Vector3 vecTowardsPlayer = (new Vector3(playerPosition.x - position.x,
            playerPosition.y - position.y)).normalized;
        transform.Translate(vecTowardsPlayer * (gameManager.enemySpeed * Time.deltaTime));
    }


    // Deactivate Player if Enemy collides with player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.SetActive(false);
        gameManager.ToGameOverScreen();
    }

    // Can't destroy player within 0.6f seconds of spawning
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(0.6f);
        if (!gameManager.hasFreeze)
        {
            spriteRenderer.color = Color.white;
        }

        enemyCollider.isTrigger = false;
        canMove = true;
    }
}