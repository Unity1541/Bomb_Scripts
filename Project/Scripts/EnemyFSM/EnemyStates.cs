using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyStates 
{
    public EnemyController  enemyController;
    public EnemyStateMachine enemyStateMachine;
    public NavMeshAgent agent;
    public EnemyStates(EnemyController _enemyController, EnemyStateMachine _enemyStateMachine)
    {//既然這邊有令參數，那麼base那邊就一定要加上去
        enemyController = _enemyController;
        enemyStateMachine = _enemyStateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
