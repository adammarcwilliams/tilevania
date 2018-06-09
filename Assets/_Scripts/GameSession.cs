using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    int score = 0;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}

    public void AddToScore (int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
	
	public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
            livesText.text = playerLives.ToString();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    private void TakeLife()
    {
        playerLives --;

        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
