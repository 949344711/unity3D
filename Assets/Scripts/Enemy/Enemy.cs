using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 敌人自动巡逻
/// </summary>
public class Enemy : MonoBehaviour
{  
    //1.移动状态 0 1 2
    //2控制是否产生随机点
    //3.控制移动状态能否改变的状态
    Actions ani;
    //Animation ani;//动画控制组件
    NavMeshAgent NMA;//导航控制组件
    public GameObject player;//玩家对象
    float distance;//玩家和敌人之间的距离
    int nowstate;//当前敌人所处的状态
    bool isRandom;//能否随机一个坐标点
    float stoptime;//从移动改变为停止的状态改变的时刻
    bool isChangestate;//控制能否改变状态
    //随机到的两个数
    float x;
    float z;
    public float Xstart, Xend, Zstart, Zend;
    //移动的时间
    float movetime;


    //关于伤害
    private float pistalShootTimer = 0;
    public Transform muzzlePoint;
    public LayerMask layer;
    public AudioClip pistolSound;
    public LineRenderer line;

    // Use this for initialization
    void Start()
    {
        NMA = gameObject.GetComponent<NavMeshAgent>();//获取导航组件
        //敌人当前所处状态为0（发呆动画、停止不动）
        nowstate = 0;
        //可以随机一个数
        isRandom = true;
        //停留时间记为0
        stoptime = 0;
        //可以改变状态
        isChangestate = true;
        //获取动画组件
        ani = gameObject.GetComponent<Actions>();
        muzzlePoint = this.transform.Find("MuzzlePoint").GetComponent<Transform>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家和敌人之间的距离，实时更新
        distance = Vector3.Distance(transform.position, player.transform.position);
        //调用距离改变函数
        distancechange();
        //调用状态改变函数
        statechange();
    }
    /// <summary>
    /// 距离改变函数
    /// </summary>
    void distancechange()
    {
        if (distance <= 30)//如果玩家和敌人之间距离小于30
        {
            isChangestate = true;//可以改变状态
            nowstate = 2;//敌人状态为追赶、攻击玩家
        }
        else if (distance > 30)//如果距离大于30
        {
            if (isChangestate == true)//如果可以改变状态
            {
                nowstate = 0;//状态改变为自动巡逻的发呆状态（播放发呆动画）
                stoptime = 0;//记下状态改变的时刻
                isChangestate = false;//状态不可以改变了
            }
        }

    }
    /// <summary>
    /// 状态改变函数
    /// </summary>
    void statechange()
    {
        if (nowstate == 0)//如果状态为0
        {
            ani.Stay();//播放发呆动画
            NMA.isStopped = true;
            //NMA.Stop();//导航停止
            stoptime += Time.deltaTime;//停留时间自增
            isRandom = true;//可以随机数
            if (stoptime >= 2)//如果停留了2秒
            {
                nowstate = 1;//状态变为1
            }
        }
        else if (nowstate == 1)//如果状态为1
        {
            NMA.isStopped = false;//导航恢复
            if (isRandom == true)//如果可以随机数
            {
                //随机两个点（地形范围内）
                x = Random.Range(Xstart, Xend);
                z = Random.Range(Zstart, Zend);

                isRandom = false;//只允许随机两个点
            }
            //下一个坐标点为随机到的点
            Vector3 nextpos = new Vector3(x, transform.position.y, z);
            NMA.destination = nextpos; //导航到下一个点
            ani.Walk();//播放走路动画
            //Debug.DrawLine(transform.position, nextpos, Color.red);//从当前位置到下一个点之间画一条红线（红线在Scene场景中可见）
            if (NMA.remainingDistance <= 0.1f)//敌人和下一个点之间的距离小于0.1
            {
                nowstate = 0;//状态变为0
                stoptime = 0;//停留时间置为0
            }
        }
        else if (nowstate == 2)//如果状态为2
        {
            NMA.destination = player.transform.position;//导航到玩家位置
            if (NMA.remainingDistance > 20)//如果和玩家距离大于10
            {
                NMA.isStopped = false;
                ani.Run();//播放跑动画
            }
            else if (NMA.remainingDistance <= 20)//如果和玩家之间距离小于10
            {
                NMA.isStopped = true;//导航停止
                transform.LookAt(player.transform);//敌人看向玩家
                Shoot();
            }
            //Debug.DrawLine(transform.position,player.transform.position,Color.red);//敌人和玩家之间画一条红线
        }
    }



    void Shoot()
    {
        RaycastHit hit;
        Vector3 dir = player.transform.position - muzzlePoint.position + new Vector3(0, 0.5f, 0);
        pistalShootTimer -= Time.deltaTime;
        if(Physics.Raycast(muzzlePoint.position,dir,out hit,layer))
        {
            Debug.DrawLine(muzzlePoint.position, hit.transform.position, Color.blue);
            //Debug.DrawRay(muzzlePoint.position, dir, Color.blue);
            if (hit.transform.tag.Equals("Player") && pistalShootTimer <= 0)
            {
                pistalShootTimer = 1f;
                ani.Attack();
                line.SetPositions(new Vector3[] { muzzlePoint.position, hit.point });
                GetComponent<AudioSource>().PlayOneShot(pistolSound);
                hit.transform.GetComponent<PlayerHurt>().OnDamage(10);
            }
        }
    }
}