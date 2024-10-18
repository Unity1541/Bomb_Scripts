using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening; 

public class EnemyController : MonoBehaviour
{
    [Header("攻擊方向相關")]
    public Transform playerTransform;
    public Vector3 targetDirection;
    public Transform attackTransform;
    public AnimationCurve animationCurve;

    [Header("程式碼互動區")]
    public EnemyStatus enemyStatus;
    public bool isHit;
    
    [Header("人物屬性相關")]
    public bool enemyIsGround;
    public Animator animator;
    public Rigidbody rigidbody;
    public NavMeshAgent navMeshAgent;
    public Material material;
    public Renderer renderer;
    [Space]
    [Header("State相關")]
    public EnemyIdle motionFSM;
    public EnemyWalk enemyWalk;
    public EnemyAttack enemyAttack;
    public EnemyStateMachine enemyStateMachine;
    public Transform []waypoints;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStateMachine = new EnemyStateMachine();//先建立物件，啟動一開始的狀態
        motionFSM = new EnemyIdle(this,enemyStateMachine,animator);//建構函數直接放入參數
        enemyWalk = new EnemyWalk(this,enemyStateMachine,animator,navMeshAgent);
        enemyAttack = new EnemyAttack(this,enemyStateMachine,animator,navMeshAgent);
        enemyStateMachine.Initialize(motionFSM);//一開始先放入idleState
        material = renderer.material;
        material.DOFloat(0.2f, "_Opacity", 0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (playerTransform.position-this.transform.position).normalized;
        this.isHit = enemyStatus.isHit;
        
        enemyStateMachine.currentState.HandleInput();
        enemyStateMachine.currentState.LogicUpdate();
        enemyStateMachine.currentState.PhysicsUpdate();
    }

 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enemyIsGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
   {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enemyIsGround = false;
        }
   }
}
