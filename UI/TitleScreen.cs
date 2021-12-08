using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject player;
    private GameManager gameManager;
    private Player playerSaveData;
    
    private void Start()
    {
        playerSaveData = player.GetComponent<Player>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        playerSaveData.LoadPlayer();
        gameManager.ToDifficultyScreen();
    }
}
