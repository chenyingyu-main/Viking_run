using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text timeText;
    private bool timerActive;

    int score = 0;
    float time = 0f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + " COINS";
        timeText.text = "SURVIVAL TIME: " + time.ToString() + " s";
        timerActive = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timerActive = !timerActive;
        }
        if (timerActive)
        {
            time += Time.deltaTime;
            timeText.text = "SURVIVAL TIME: " + $"{(int)time}" + " s";
        }
    }
    public void AddPoints()
    {
        score += 1;
        scoreText.text = score.ToString() + " COINS";
    }
}
