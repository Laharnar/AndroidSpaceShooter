using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour {

    internal static UIStats m;

    public Text scoreText;
    public Text scorePlayText;
    public Text hpText;
    public Text scoreMaxText;
    public Image progressBar;

    public Transform gameOverGroup;
    public Transform gamePlayGroup;
    public Transform mainMenuGroup;
    public Transform winLevelGroup;
    public Transform newMaxScoreGroup;

    Transform[] menuGroups;


    internal void EnableGroup(int groupIndex, bool value = true, bool invertPreviousGroups = true)
    {
        if (invertPreviousGroups)
        {
            for (int i = 0; i < menuGroups.Length; i++)
            {
                if (menuGroups[i] != null)
                    menuGroups[i].gameObject.SetActive(!value);
                else Debug.Log("Group on index " + i + " wasn't assigned, so we can't disable it.");
            }
        }
        menuGroups[groupIndex].gameObject.SetActive(value);
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (m && m != this)
        {
            Destroy(m.gameObject);
        }
        m = this;

        //DontDestroyOnLoad(gameObject);

        menuGroups = new []{ gameOverGroup, gamePlayGroup, mainMenuGroup, winLevelGroup, newMaxScoreGroup };
    }

	// Update is called once per frame
	void OnGUI () {
        GUI();    
    }

    /// <summary>
    /// Handles GUI text updating and bar.
    /// 
    /// Note: In case you want to extend this behaviour derive that MonoBehaviour from this class.
    /// </summary>
    protected virtual void GUI()
    {
        scorePlayText.text = "" + Game.GetScore();
        scoreText.text = "" + Game.GetScore();
        scoreMaxText.text = "" + Game.maxScore;
        hpText.text = PlayerPilot.player != null ? "" + PlayerPilot.player.sship.hp : "";

        progressBar.fillAmount = ((float)Game.GetLevelScore() / (float)Game.TargetScore());
        if (progressBar.fillAmount == 1)
        {
            progressBar.fillAmount = 0;
            Game.FinishLevel();
        }
    }

    /// <summary>
    /// Display "Game over" UI
    /// 
    /// Tag: lose
    /// </summary>
    internal void ShowGameOver()
    {
        m.EnableGroup(0);

        //if we got new best, display "NEW" beside its text
        m.EnableGroup(4, Game.m.newBest, false);
    }

    public void ShowGamePlay()
    {
        m.EnableGroup(1);
    }

    internal void ShowMainMenu()
    {
        m.EnableGroup(2);
    }
    
    internal void ShowWinLevelSign(bool active)
    {
        m.EnableGroup(3, active, false);
    }
}
