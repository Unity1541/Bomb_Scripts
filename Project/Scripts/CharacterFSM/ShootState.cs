using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : States
{
    bool grounded;
    bool shooting;
    bool holdGun;
    public ShootState(CharacterController _character,StateMachine _stateMachine) : base(_character,_stateMachine)
    {
        characterController = _character;//先取得繼承State而來的characterController,stateMachine的初始化參數
        stateMachine = _stateMachine;
    }


    public override void Enter()
    {
        base.Enter();
        grounded = characterController.characterIsGround;
        characterController.animator.SetTrigger("Shooting");
    }

    public override void HandleInput()
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(grounded)
        {
            stateMachine.ChangeState(characterController.shootIdle);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
