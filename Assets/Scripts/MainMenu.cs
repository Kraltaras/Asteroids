using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputField playerName;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameMenu")
        {
            GameController controller = FindObjectOfType<GameController>();
            if (controller.PlayerName != null && controller.PlayerName != "")
            {
                playerName.text = controller.PlayerName;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        if (playerName.text == null || playerName.text == "")
        {
            playerName.text = "Player";
        }
        FindObjectOfType<GameController>().StartGame(playerName.text);
        SceneManager.LoadScene("Level");
    }

    public void ViewScore()
    {
        SceneManager.LoadScene("ScoreMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGameOK()
    {
        SceneManager.LoadScene("ScoreMenu");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
