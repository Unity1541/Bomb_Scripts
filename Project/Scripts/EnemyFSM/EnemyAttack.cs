using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttack : EnemyStates
{
    bool IsAttack,IsIdle,IsWalk;
    float timer=1f;
    Animator animator;
    public EnemyAttack(EnemyController _controller, EnemyStateMachine _stateMachine,Animator _animator,NavMeshAgent _navMeshAgent):base(_controller, _stateMachine)
    {
            enemyController = _controller;
            enemyStateMachine = _stateMachine;
            animator = _animator;
            agent = _navMeshAgent;
    }

    public override void Enter()
    {
        //   Vector3 target = enemyController.targetDirection;
        //   Quaternion rotation = Quaternion.LookRotation(target, Vector3.up);
        //   enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation,rotation,10f);
          base.Enter();
          IsAttack=true;
          IsIdle=false;
          IsWalk=false;
    }

    public override void HandleInput()//因為電腦AI無法按按鈕，因此改用條件方式調整bool值
    {
       
        if(timer>=1)
        {  
            timer -=Time.deltaTime;
            animator.SetTrigger("IsAttack");      
        }
        else
        {
            IsAttack=false;
            IsIdle=true;
            timer = 1f;
        }
    }
    public override void LogicUpdate()
    {
        //Vector3 target = enemyController.targetDirection;
        //Quaternion rotation = Quaternion.LookRotation(target, Vector3.up);
        // enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation,rotation,rotationSpeed);
        
        // Vector3 target = enemyController.targetDirection;
        // float targetAngle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
        // float referenceVelocity=12.0f;
        // float currentAngle = enemyController.transform.eulerAngles.y;
        // float smoothAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle,ref referenceVelocity,0.08f);
        // enemyController.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);


         if(IsIdle)
         {
            enemyStateMachine.ChangeState(enemyController.motionFSM);
         }
    }
       


    public override void PhysicsUpdate()
    {

       
    }

    public override void Exit()
    {
    }
}



