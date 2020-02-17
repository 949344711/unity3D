using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform muzzlePoint;
    public Camera camer;
    public GameObject prefabRifleBullet;
    public GameObject prefabSniperBullet;
    public LayerMask layer; // 射击时射线能射到的碰撞层
    Actions actions;
    private float sniperShootTimer = 0;
    public AudioClip sniperSound;
    public AudioClip rifleSound;
    // Use this for initialization
    void Start()
    {
        camer = Camera.main;
        actions = GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        sniperShootTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.nowPlayerState != GameManager.PlayerState.Empty)
        {
            RaycastHit hit;
            Ray ray = camer.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layer))
            {
                if (GameManager.Instance.nowPlayerState == GameManager.PlayerState.Rifle)
                {
                    actions.Attack();
                    GetComponent<AudioSource>().PlayOneShot(rifleSound);
                    GameObject go = GameObject.Instantiate(prefabRifleBullet, muzzlePoint.position, Quaternion.identity);
                    //计算方向.
                    Vector3 dir = hit.point - muzzlePoint.position;
                    //Debug绘制射线.
                    Debug.DrawRay(muzzlePoint.position, dir, Color.green);
                    //发射子弹.
                    go.GetComponent<Rigidbody>().AddForce(dir * 1000);
                    if (hit.transform.tag.Equals("Enemy"))
                    {
                        // 敌人减少生命
                        hit.transform.GetComponent<EnemyHurt>().OnDamage(25);
                    }
                }
                else if(GameManager.Instance.nowPlayerState == GameManager.PlayerState.Sniper || GameManager.Instance.nowPlayerState == GameManager.PlayerState.SightOpen)
                {
                    if (sniperShootTimer <= 0)
                    {
                        actions.Attack();
                        GetComponent<AudioSource>().PlayOneShot(sniperSound);
                        sniperShootTimer = 0.8f;
                        GameObject go = GameObject.Instantiate(prefabSniperBullet, muzzlePoint.position, Quaternion.identity);
                        //计算方向.
                        Vector3 dir = hit.point - muzzlePoint.position;
                        //Debug绘制射线.
                        Debug.DrawRay(muzzlePoint.position, dir, Color.green);
                        //发射子弹.
                        go.GetComponent<Rigidbody>().AddForce(dir * 100);
                        if (hit.transform.tag.Equals("Enemy"))
                        {
                            hit.transform.GetComponent<EnemyHurt>().OnDamage(100);
                        }
                    }
                }
            }
        }
    }
}
