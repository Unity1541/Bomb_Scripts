using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public States currentState;//目標要追蹤現在的程式狀態

    public void Initialize(States startingState)
    //在Character程式碼當中可以看到一開始的movementSM.Initialize(standing)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(States newState)
    {
        currentState.Exit();//由這邊執行個別state裡面的Exit方法，更新參數後

        currentState = newState;
        newState.Enter();//一旦進入新狀態之後，就會更新所需要的參數,其它logicUpdate(),PhysicsUpdate()在CharacterController更新
    }
}
