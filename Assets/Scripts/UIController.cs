using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text playerHP;
    [SerializeField] private Text scoreText;

    public const float adjustVertPos = -3.0f; //the variable to adjust vertical position of player HP if its visual indicator isn't centered

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        ChangeHealthPos(player, adjustVertPos);

        playerHP.text = player.GetComponent<ShipHealth>().Health.ToString();
        switch (playerHP.text) //player HP is changing color when it is in dangerous state or full
        {
            case "15":
                playerHP.color = Color.green;
                break;
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
                playerHP.color = Color.red;
                break;
            default:
                playerHP.color = Color.white;
                break;
        }

        GameController gameScore = FindObjectOfType<GameController>();
        scoreText.text = gameScore.Score.ToString();
    }

    public void ChangeHealthPos(GameObject player, float adjustVertPos) //player HP on GUI must have the same position as player
    {
        RectTransform canvasRect = GetComponent<RectTransform>();

        float cameraWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height; //screen width in unity points
        float newPosY = (Camera.main.orthographicSize + player.transform.position.y) * canvasRect.rect.height / (Camera.main.orthographicSize * 2);
        float newPosX = (cameraWidth / 2 + player.transform.position.x) * canvasRect.rect.width / cameraWidth;
        playerHP.transform.position = new Vector3(newPosX, newPosY + adjustVertPos, 0);
    }
}
