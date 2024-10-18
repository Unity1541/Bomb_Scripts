using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : States
{//每一個個別狀態都會有各自要更新的參數，不會和其它狀態的參數混淆在一起

    bool throwbomb;
    bool attack;
    bool jump;
    bool grounded;
    bool sprint;
    float playerSpeed;
    public AttackState(CharacterController _character,StateMachine _stateMachine) : base(_character,_stateMachine)
    {
        characterController = _character;//先取得繼承State而來的characterController,stateMachine的初始化參數
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        grounded = characterController.characterIsGround;
        
        if (attackAction.triggered)
        {
             attack = true;
             characterController.animator.SetTrigger("Attack");
        }
       //加入丟手榴彈
        else if (throwAction.triggered)
        {
            throwbomb = true;
            characterController.animator.SetTrigger("ThrowBomb");
        }
        
    }

     public override void HandleInput()
    {
        base.HandleInput();
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(grounded)
        {
            stateMachine.ChangeState(characterController.standing);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
       
    }

    public override void Exit()
    {
        attack=false;
        throwbomb=false;
        base.Exit();
    }
}
