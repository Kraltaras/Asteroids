using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighScore : MonoBehaviour
{
    private Transform _textContainer;
    private Transform _textTemplate;

    private void Awake()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (gameController != null)
        {
            if (gameController.newRecord)
            {
                gameController.AddNewScore(gameController.PlayerName, gameController.Score);
                gameController.SaveHighscore();
                gameController.newRecord = false;
            }

            _textContainer = transform.Find("HighscoreContainer");
            _textTemplate = _textContainer.Find("HighscoreTextTemplate");
            _textTemplate.gameObject.SetActive(false);

            float templateHeight = 60f;
            for (int i = 0; i < 10; i++)
            {
                Transform entry = Instantiate(_textTemplate, _textContainer);
                RectTransform entryRect = entry.GetComponent<RectTransform>();
                entryRect.anchoredPosition = new Vector2(0, -templateHeight * i);
                entry.gameObject.SetActive(true);

                int rank = i + 1;

                entry.Find("TextPlayer").GetComponent<Text>().text = gameController.thisGameHighscore[i].name;
                entry.Find("TextPosition").GetComponent<Text>().text = rank.ToString();
                entry.Find("TextScore").GetComponent<Text>().text = gameController.thisGameHighscore[i].score.ToString();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
