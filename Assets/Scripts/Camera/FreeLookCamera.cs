using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCamera : MonoBehaviour {
    CinemachineFreeLook freeLook;
	// Use this for initialization
	void Start () {
        freeLook = GetComponent<CinemachineFreeLook>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.nowPlayerState != GameManager.PlayerState.SightOpen)
        {
            freeLook.m_Lens.FieldOfView = 50;
        }
        else
        {
            freeLook.m_Lens.FieldOfView = 5;
        }
        if (GameManager.Instance.nowPlayerState == GameManager.PlayerState.SightOpen)
        {
            freeLook.m_XAxis.m_MaxSpeed = 30;
            freeLook.m_YAxis.m_MaxSpeed = 0.5f;
        }
        else
        {
            freeLook.m_XAxis.m_MaxSpeed = 300;
            freeLook.m_YAxis.m_MaxSpeed = 2;
        }
    }
}
