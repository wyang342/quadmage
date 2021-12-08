using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    private GameManager gameManager;
    [SerializeField] private GameObject enemyProjectile;

    private void Awake()
    {
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
        StartCoroutine(FireRoutine());
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
    }

    // Recursively Fires enemyProjectile
    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(gameManager.enemy1FireRate);
        if (!gameManager.isGameActive) yield break;
        if (!gameManager.hasFreeze)
        {
            Instantiate(enemyProjectile, transform.position, enemyProjectile.transform.rotation);
        }

        StartCoroutine(FireRoutine());
    }
}