using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float speed = 10f;
    private float verticalInput;
    private float horizontalInput;

    private SpriteRenderer sprite;
    private GameManager gameManager;
    private Animator playerAnimator;

    // Declare arrow variables
    private GameObject up;
    private GameObject right;
    private GameObject down;
    private GameObject left;

    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnimator = GetComponent<Animator>();

        // Get children that show direction
        up = transform.Find("Up").gameObject;
        right = transform.Find("Right").gameObject;
        down = transform.Find("Down").gameObject;
        left = transform.Find("Left").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
        CheckFireInput();
        Arrows();
    }

    private void MovePlayer()
    {
        verticalInput = Input.GetAxis("Vertical") * speed;
        horizontalInput = Input.GetAxis("Horizontal") * speed;
        playerAnimator.SetBool("GoingUp", verticalInput > 0);
        transform.Translate(new Vector3(horizontalInput, verticalInput) * Time.deltaTime);
    }

    void ConstrainPlayerPosition()
    {
        if (transform.position.x > gameManager.horBound)
        {
            transform.position = new Vector3(gameManager.horBound, transform.position.y, -1);
        }
        else if (transform.position.x < -gameManager.horBound)
        {
            transform.position = new Vector3(-gameManager.horBound, transform.position.y, -1);
        }

        if (transform.position.y > gameManager.vertBound)
        {
            transform.position = new Vector3(transform.position.x, gameManager.vertBound, -1);
        }
        else if (transform.position.y < -gameManager.vertBound)
        {
            transform.position = new Vector3(transform.position.x, -gameManager.vertBound, -1);
        }
    }

    void CheckFireInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.allowFire && !gameManager.hasDuo && !gameManager.hasGhost)
        {
            sprite.color = Color.grey;
            Instantiate(bulletPrefab, transform.position,
                bulletPrefab.transform.rotation);
            
            gameManager.allowFire = false;
            StartCoroutine(BulletRoutine());
            gameManager.bulletDir++;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && gameManager.allowFire && gameManager.hasDuo)
        {
            sprite.color = new Color(255, 0, 0, 0.5f);
            if (gameManager.bulletDir % 2 == 0)
            {
                Instantiate(bulletPrefab, transform.position + new Vector3(0.75f, 0, 0),
                    bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, transform.position + new Vector3(-0.75f, 0, 0),
                    bulletPrefab.transform.rotation);
            }
            else
            {
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.75f, 0),
                    bulletPrefab.transform.rotation);
                Instantiate(bulletPrefab, transform.position + new Vector3(0, -0.75f, 0),
                    bulletPrefab.transform.rotation);
            }

            gameManager.allowFire = false;
            StartCoroutine(BulletRoutine());
            gameManager.bulletDir++;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && gameManager.allowFire && gameManager.hasGhost)
        {
            sprite.color = new Color(0,0,0,0.5f);
            Instantiate(bulletPrefab, transform.position,
                bulletPrefab.transform.rotation);
            
            gameManager.allowFire = false;
            StartCoroutine(BulletRoutine());
            gameManager.bulletDir++;
        }
    }

    IEnumerator BulletRoutine()
    {
        yield return new WaitForSeconds(0.9f);
        gameManager.allowFire = true;
        if (!gameManager.hasDuo && !gameManager.hasGhost)
        {
            sprite.color = Color.white;
        }
        else if (gameManager.hasDuo)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = new Color(0,0,0);
        }
    }

    // Activate/deactivate directional arrows for easier gameplay
    private void Arrows()
    {
        switch (gameManager.bulletDir % 4)
        {
            case 0:
                up.SetActive(true);
                right.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                break;
            case 1:
                up.SetActive(false);
                right.SetActive(true);
                down.SetActive(false);
                left.SetActive(false);
                break;
            case 2:
                up.SetActive(false);
                right.SetActive(false);
                down.SetActive(true);
                left.SetActive(false);
                break;
            case 3:
                up.SetActive(false);
                right.SetActive(false);
                down.SetActive(false);
                left.SetActive(true);
                break;
        }
    }
}