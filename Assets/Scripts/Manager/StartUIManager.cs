using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour {
    public Button StartButton;
    public Button ExitButton;
	// Use this for initialization
	void Start () {
        StartButton.onClick.AddListener(ClickStartButton);
        ExitButton.onClick.AddListener(ClickExitButton);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void ClickStartButton()
    {
        GameManager.Instance.nowGameState = GameManager.GameState.Play;
        GameManager.Instance.playerHP = 100;
        GameManager.Instance.nowGameState = GameManager.GameState.Play;
        SceneManager.LoadScene("Lucky");
    }

    void ClickExitButton()
    {
        Application.Quit();
    }
}
