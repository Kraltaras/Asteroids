using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip levelBGM;
    [SerializeField] AudioClip menuBGM;

    private static GameController _instance;
    private string _filename;

    public HighScore[] thisGameHighscore = new HighScore[10];

    public static GameController instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public int Score { get; set; }
    public string PlayerName { get; set; }

    private bool _paused;
    public bool newRecord;

    private void Awake()
    {
        if(instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _paused = false;
        musicSource.clip = menuBGM;
        musicSource.Play();
        _filename = Path.Combine(Application.persistentDataPath, "score.dat");
        LoadHighscore();
        newRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PauseGame();
            }
            if((Input.GetKeyDown(KeyCode.Escape)) || (GameObject.FindGameObjectWithTag("Player").GetComponent<ShipHealth>().Health == 0))
            {
                EndGame();
                SceneManager.LoadScene("EndGame");
            }
            newRecord = true;
        }
        if(SceneManager.GetActiveScene().name == "EndGame")
        {
            Text textPlayer = GameObject.Find("TextPlayer").GetComponent<Text>();
            Text textScore = GameObject.Find("TextScore").GetComponent<Text>();
            textPlayer.text = PlayerName;
            textScore.text = Score.ToString();
        }
    }

    public void StartGame(string playerName)
    {
        musicSource.Stop();
        Score = 0;
        PlayerName = playerName;
        musicSource.clip = levelBGM;
        musicSource.Play();
        Cursor.visible = false;
    }

    public void EndGame()
    {
        musicSource.Stop();
        musicSource.clip = menuBGM;
        musicSource.Play();
        Cursor.visible = true;
    }

    public void IncreaseScore(int scoreValue)
    {
        Score += scoreValue;
    }

    public void PauseGame()
    {
        if(!_paused)
        {
            Time.timeScale = 0;
            musicSource.Pause();
            _paused = true;
        }
        else
        {
            Time.timeScale = 1;
            musicSource.Play();
            _paused = false;
        }
    }

    public void LoadHighscore()
    {
        if(!File.Exists(_filename))
        {
            for(int i = 0; i < 10; i++)
            {
                thisGameHighscore[i] = new HighScore("Player", 0);
            }
            return;
        }

        using (FileStream stream = File.Open(_filename, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            thisGameHighscore = formatter.Deserialize(stream) as HighScore[];
        }
    }

    public void SaveHighscore()
    {
        using (FileStream stream = File.Create(_filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, thisGameHighscore);
        }
    }

    public void AddNewScore(string newName, int newScore)
    {
        int currentPos = 0;
        bool recorded = false;
        while(!recorded && currentPos < 10)
        {
            if(newScore > thisGameHighscore[currentPos].score)
            {
                for(int i = 9; i > currentPos; i--)
                {
                    thisGameHighscore[i] = thisGameHighscore[i - 1];
                }
                thisGameHighscore[currentPos] = new HighScore(newName, newScore);
                recorded = true;
            }
            currentPos++;
        }
    }
}

[Serializable()]
public struct HighScore
{
    public string name;
    public int score;

    public HighScore(string newName, int newScore)
    {
        name = newName;
        score = newScore;
    }
}