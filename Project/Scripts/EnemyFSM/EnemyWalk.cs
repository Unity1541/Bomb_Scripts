using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : EnemyStates
{
    bool IsAttack,IsIdle;
    int randomIndex;
    float timer;
    Animator animator;
    public EnemyWalk(EnemyController _controller,EnemyStateMachine _stateMachine,Animator _animator,NavMeshAgent _navMeshAgent) : base(_controller, _stateMachine)
    {
        enemyController = _controller;
        enemyStateMachine = _stateMachine;
        animator= _animator;
        agent = _navMeshAgent;
    }

     public override void Enter()
    {
          base.Enter();
          IsAttack=false;
          IsIdle=false;
    
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        timer += Time.deltaTime;
        randomIndex = Random.Range(0, enemyController.waypoints.Length);
        if(timer > 1.5f)//有兩秒時間移動到目的地，超過兩秒就開始檢查距離
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                    Debug.Log("已到達目標");
                    timer=0.0f;
                    //IsIdle = true;
                    IsAttack = true;          
            }
        }
        else{
               animator.SetFloat("Speed", 1);
               agent.SetDestination(enemyController.waypoints[randomIndex].position);
        }
        //隨時都轉向玩家
        Vector3 target = enemyController.targetDirection;
        Quaternion targetRotation = Quaternion.LookRotation(target);
        float curveValue = enemyController.animationCurve.Evaluate(Time.time);
        enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation, targetRotation,curveValue * Time.deltaTime);
  
    }

    public override void PhysicsUpdate()
    {
       
          if(IsIdle)
            enemyStateMachine.ChangeState(enemyController.motionFSM);

          if(IsAttack)
            enemyStateMachine.ChangeState(enemyController.enemyAttack);
    }

    public override void Exit()
    {
       float currentSpeed =1;
       currentSpeed = Mathf.Lerp(currentSpeed,-1f,0.1f*Time.deltaTime);
       animator.SetFloat("Speed", currentSpeed);
    }
}
