using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening; 

public class CharacterController : MonoBehaviour
{
    public Renderer renderer;
    Material material;
    public Camera camera;
    public float gravityValue = -9.81f;
    public float jumpHeight = 15f;
    public float playerSpeed = 5.0f;
    public bool characterIsGround;
    public Transform cameraTransform;
    public Vector3 targetDirection;
    public AnimationSpeed animationSpeed;
    public Transform enemyTransform;
    public Animator animator;
    public PlayerInput playerInput;
    public Rigidbody rb;
    public StateMachine movementSM;
    public StandingState standing;
    public AttackState attacking;
    public ShootIdle shootIdle;
    public ShootState shootState;
    //public LandState landing;
    void Start()//一開始都會把所以會用到的狀態機先放入參數this,stateMachine實作化
    {
        material = renderer.material;
        material.DOFloat(0.2f, "_Opacity", 0.2f).SetLoops(-1, LoopType.Yoyo);
        // 而DoTween 動畫在開始後會持續執行，無需重複調用。
        // 動畫在Start或Awake 中設置：這些方法在物件初始化時調用一次，是設置動畫的合適地方。

        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        movementSM = new StateMachine();//這邊只是實作一個空的StateMachine，當作參數給standing的建構函數裡面
        shootIdle = new ShootIdle(this,movementSM);
        shootState = new ShootState(this,movementSM);
        standing = new StandingState(this, movementSM);
        attacking = new AttackState(this,movementSM);
        //landing = new LandState(this,movementSM);
        //這樣表示當StandingState實作之後，放入兩個參數(CharacterController,StateMachine)
        //對應StandingState裡面的構造函數應對那兩著參數
        //這邊的this就是本身的CharacterController類別，實作化放入參數，只是為了取得characterController,和stateMachine
        movementSM.Initialize(standing);
    }

    void Update()
    {
        
        targetDirection = (enemyTransform.position - this.transform.position).normalized;//更新旋轉位子給ShootIdle用的  
        movementSM.currentState.HandleInput();//不斷更新在該狀態下，玩家的input數值，移動之類的
        movementSM.currentState.LogicUpdate();//不斷更新狀態，調用該狀態的方法與邏輯
        //movementSM.currentState.PhysicsUpdate();
           
    }

    void FixedUpdate()
    {     
        movementSM.currentState.PhysicsUpdate();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            characterIsGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
   {
        if (collision.gameObject.CompareTag("Ground"))
        {
            characterIsGround = false;
        }
   }
}
