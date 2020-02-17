using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform muzzlePoint;
    public Camera camer;
    public GameObject prefabBullet;

    // Use this for initialization
    void Start()
    {
        camer = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camer.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = GameObject.Instantiate(prefabBullet, muzzlePoint.position, Quaternion.identity) as GameObject;
                //计算方向.
                Vector3 dir = hit.point - muzzlePoint.position;

                //Debug绘制射线.
                Debug.DrawRay(muzzlePoint.position, dir, Color.green);

                //发射子弹.
                go.GetComponent<Rigidbody>().AddForce(dir * 1000);
            }
        }
    }
}
