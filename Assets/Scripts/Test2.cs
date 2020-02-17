using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour
{
    public float turnspeed = 10;
    public Camera mainCamera;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = mainCamera.transform.TransformDirection(Vector3.forward);
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        var eulerY = quaDir.eulerAngles.y;
        var euler = new Vector3(0, eulerY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), Time.deltaTime * turnspeed);
    }
}










