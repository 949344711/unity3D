using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 敌人自动巡逻
/// </summary>
public class EnemySniper : MonoBehaviour
{
    Actions ani;
    public GameObject player;//玩家对象
    float distance;//玩家和敌人之间的距离
    int nowstate;//当前敌人所处的状态
    Transform muzzlePoint;
    public float sniperShootTimer = 0;
    public LayerMask layer;
    public AudioClip sniperSound;
    public LineRenderer line;

    // Use this for initialization
    void Start()
    {
        ani = gameObject.GetComponent<Actions>();
        muzzlePoint = this.transform.Find("MuzzlePoint").GetComponent<Transform>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家和敌人之间的距离，实时更新
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 30)
        {
            transform.LookAt(player.transform);
            Shoot();
        }
        else
        {
            ani.Stay();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 dir = player.transform.position - muzzlePoint.position + new Vector3(0, 0.5f, 0);
        sniperShootTimer -= Time.deltaTime;
        if (Physics.Raycast(muzzlePoint.position, dir, out hit, layer))
        {
            Debug.DrawRay(muzzlePoint.position, dir, Color.blue);
            if (hit.transform.tag.Equals("Player") && sniperShootTimer <= 0)
            {
                sniperShootTimer = 2f;
                ani.Attack();
                line.SetPositions(new Vector3[] { muzzlePoint.position, hit.point });
                GetComponent<AudioSource>().PlayOneShot(sniperSound);
                hit.transform.GetComponent<PlayerHurt>().OnDamage(34);
            }
        }
    }
}