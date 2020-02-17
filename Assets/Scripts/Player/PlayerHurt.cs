using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    Actions actions;
	// Use this for initialization
	void Start () {
        actions = GetComponent<Actions>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.playerHP <= 0)
        {
            GameManager.Instance.nowGameState = GameManager.GameState.Death;
        }
	}

    public void OnDamage(float hurtValue)
    {
        actions.Damage();
        GameManager.Instance.playerHP -= hurtValue;
    }
}
