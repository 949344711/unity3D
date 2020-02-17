using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour {
    public Image Win;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PlayerRifleBullet" || collision.collider.tag == "PlayerSniperBullet")
        {
            Win.gameObject.SetActive(true);
            Invoke("GoBack", 5);
        }
    }

    void GoBack()
    {
        SceneManager.LoadScene("Start");
    }
}
