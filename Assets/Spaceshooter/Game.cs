using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour {

    public static Game m;

    public UIStats ui;

    //max score in ALL play
    internal static int maxScore { get; private set; }

    /// <summary>
    /// Tell if current score is also max.
    /// </summary>
    internal bool newBest { get { return maxScore < playScore; } }

    /// <summary>
    /// how long will be "passed level" visible after level is done
    /// </summary>
    static float passLevelDisplayTime = 5f;
    static int targetScore = 10;
    int playScore = 0;//one time play's score

    int score = 0;//level's score, gets reset after every level
    int scoreBuffer = 0;


    bool isBarFull { get { return targetScore / score >= 1; } }

    internal GameState state = GameState.Playing;

    void Start()
    {
        Init();
    }

    void Init()
    {
        m = this;

        targetScore = 10;

        Time.timeScale = 1;

        state = GameState.Playing;

        if (ui == null)
        {
            Debug.Log("UIScript is null", this);
        }else
            ui.ShowGamePlay();
    }

    void Update()
    {
        UpdateScore();

        GameLoseCheck();
    }

    private void GameLoseCheck()
    {
        if (isGameOver)
        {
            //on game over effects

            //handle game over

            Debug.Log("Game is over. Score is " + Game.GetScore());

            state = GameState.WaitingInput;

            //check if it's new max score
            if (playScore > maxScore)
            {
                maxScore = playScore;
            }

            //display fail game ui
            ui.ShowGameOver();

        }
    }

    internal static int GetScore()
    {
        return m.score+m.playScore;
    }

    internal static float GetLevelScore()
    {
        return m.score;
    }

    internal static void IncreaseScore(ScoreWorth sw)
    {
        m.scoreBuffer += sw.scoreWorth;
        if (m == null)
        {
            Debug.Log("Can't update score. No Game script in scene.");

        }
    }

    void UpdateScore()
    {
        //+slowly increase score over time, by small amounts
        score = scoreBuffer;
    }

    public bool isGameOver { get { return state == GameState.Over; } }

    internal static int TargetScore()
    {
        return targetScore;
    }

    internal static void FinishLevel()
    {
        if (m.state != GameState.Playing)
            return;

        m.state = GameState.Won;

        Spawning.Disable(passLevelDisplayTime);
        m.StartCoroutine(m.FinishLevel(passLevelDisplayTime));
    }

    IEnumerator FinishLevel(float winDisplayTime)
    {
        //wait for some time while displaying awesome sign, then continue to level 2

        winDisplayTime += Time.time;

        UIStats.m.ShowWinLevelSign(true);
        
        while (winDisplayTime > Time.time)
        {
            //display level completed sign
            yield return 0;
        }

        UIStats.m.ShowWinLevelSign(false);

        ResetLevelScore();//don't reset score when level is finished
        IncreaseDificulty();

        ContinuePlay();
    }

    static void ContinuePlay()
    {
        m.state = GameState.Playing;
    }

    static void IncreaseDificulty()
    {
        targetScore = (int)(targetScore * 1.4f);
    }
    

    static void ResetLevelScore()
    {
        m.playScore += m.score;
        m.score = 0;
        m.scoreBuffer = 0;
    }
}

internal enum GameState
{
    Playing,
    Paused,
    Won,
    Over,
    WaitingInput
}