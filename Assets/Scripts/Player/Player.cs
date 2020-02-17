using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Actions actions;
    private PlayerController controller;
    private CharacterController characterController;
    public Camera camer;
    public AudioClip Run;
    public AudioClip Walk;

    int haveGun = 0;
    float speed = 5f;  //移动速度
    public float turnspeed = 10f;   //转向速度

    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        actions = GetComponent<Actions>();
        controller = GetComponent<PlayerController>();
        camer = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();
        GetWeapon();
        Move();
        AnimateControl();
        OpenSight();
    }

    void Move()
    {
        //旋转
        Vector3 dir = camer.transform.TransformDirection(Vector3.forward);
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        var eulerY = quaDir.eulerAngles.y;
        var euler = new Vector3(0, eulerY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), Time.deltaTime * turnspeed);

        //移动
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = camer.transform.TransformDirection(moveDirection);
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void AnimateControl()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (speed == 5f)
            {
                actions.Walk();
            }
            if (speed == 8f)
            {
                actions.Run();
                //AS.PlayOneShot(Run);
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if(GameManager.Instance.nowPlayerState == GameManager.PlayerState.Empty)
            {
                actions.Attack();
            }
        }
        else if(Input.GetMouseButton(1))
        {
            actions.Aiming();
        }
        else
            actions.Stay();
    }

    void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            speed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            speed = 5f;
        }
    }

    void GetWeapon()
    {
        string a;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            haveGun = 0;
            GameManager.Instance.nowPlayerState = GameManager.PlayerState.Empty;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            haveGun = 1;
            GameManager.Instance.nowPlayerState = GameManager.PlayerState.Rifle;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            haveGun = 2;
            GameManager.Instance.nowPlayerState = GameManager.PlayerState.Sniper;
        }
        a = controller.arsenal[haveGun].name;
        controller.SetArsenal(a);
    }

    void OpenSight()
    {
        if(Input.GetMouseButton(1) && GameManager.Instance.nowPlayerState == GameManager.PlayerState.Sniper)
        {
            GameManager.Instance.nowPlayerState = GameManager.PlayerState.SightOpen;
        }
        if(Input.GetMouseButtonUp(1) && GameManager.Instance.nowPlayerState == GameManager.PlayerState.SightOpen)
        {
            GameManager.Instance.nowPlayerState = GameManager.PlayerState.Sniper;
        }
    }
}