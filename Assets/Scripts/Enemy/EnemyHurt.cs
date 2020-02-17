using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour {
    public float enemyHP = 100;
    Actions actions;
	// Use this for initialization
	void Start () {
        enemyHP = 100;
        actions = GetComponent<Actions>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void OnDamage(float hurtValue)
    {
        enemyHP -= hurtValue;
        if(enemyHP > 0)
        {
            actions.Damage();
        }
    }
}
