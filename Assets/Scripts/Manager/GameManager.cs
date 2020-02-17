using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityScript;

public class GameManager : Singleton<GameManager>  {
    public float playerHP = 100;
    public GameState nowGameState;
    public PlayerState nowPlayerState;
    public enum GameState
    {
        Start, Play, Pause, Loading, Death,
    }
    public enum PlayerState
    {
        Empty,Rifle,Sniper,SightOpen,
    }
    // Use this for initialization
    void Start () {
        playerHP = 100;
	}
	
	// Update is called once per frame
	void Update () {
        GameStateAction();
        PlayerStateAction();
        if (playerHP < 0)
            playerHP = 0;
    }

    void GameStateAction()
    {
        if (nowGameState == GameState.Play)
        {
            Time.timeScale = 1;
        }
        else if (nowGameState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
    }

    void PlayerStateAction()
    {

    }
}
