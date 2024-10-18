using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyStates
{
    bool IsWalk,IsAttack,IsIdle;
    public float timer;
    Animator animator;
    public EnemyIdle(EnemyController _controller, EnemyStateMachine _stateMachine,Animator _animator):base(_controller, _stateMachine)
    {
            enemyController = _controller;
            enemyStateMachine = _stateMachine;
            animator = _animator;
    }

    public override void Enter()
    {
          timer=1.0f;
          animator.SetFloat("Speed", 0.2f);
          base.Enter();
          IsAttack=false;
          IsIdle=true;
          IsWalk=false;
    }

    public override void HandleInput()//因為電腦AI無法按按鈕，因此改用條件方式調整bool值
    {
        if(timer>=0)
        {
            timer -=Time.deltaTime;
            IsWalk=false;
        }
        else
        {
            timer=1.5f;
            IsWalk=true;
        }
    }

    public override void LogicUpdate()
    {
        if(IsWalk)
        {
             enemyStateMachine.ChangeState(enemyController.enemyWalk);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void Exit()
    {
    }
}
