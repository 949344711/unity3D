using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public Text health;
    public GameObject normalSight;
    public GameObject sniperSight;
	// Use this for initialization
	void Start () {
        GameManager.Instance.nowGameState = GameManager.GameState.Play;
	}
	
	// Update is called once per frame
	void Update () {
        health.text = GameManager.Instance.playerHP.ToString();
        OpenSight();
        if(GameManager.Instance.nowGameState == GameManager.GameState.Death)
        {
            Invoke("GoBack", 2);
        }
    }

    void OpenSight()
    {
        if(GameManager.Instance.nowPlayerState == GameManager.PlayerState.SightOpen)
        {
            normalSight.SetActive(false);
            sniperSight.SetActive(true);
        }
        else
        {
            normalSight.SetActive(true);
            sniperSight.SetActive(false);
        }
    }

    void GoBack()
    {
        SceneManager.LoadScene("Start");
    }
}
