using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : States
{
   bool grounded;
   bool sprint;
   Vector3 airVelocity;
   float gravityValue;
   float jumpHeight;
   float playerSpeed;
   bool drawWeapon;
   bool attack;   
   public JumpState(CharacterController _character, StateMachine _stateMachine) : base(_character,_stateMachine)
   {
      characterController = _character;//這邊是說繼承的State裡面的characterController
	   stateMachine = _stateMachine;//這邊是說繼承的State裡面的stateMachine
   }

   public override void Enter()
   {   
      input = Vector2.zero;//這邊的input是繼承State而來的  
      attack = false;
      base.Enter();//負責Debug看看現在在哪邊State
      grounded = false;
      gravityValue = characterController.gravityValue;
      jumpHeight = characterController.jumpHeight;
      playerSpeed = characterController.playerSpeed;
      gravityVelocity.y=0.0f;

      characterController.animator.SetFloat("Horizontal",0.1f,0.4f,Time.deltaTime);
      //不可以直接歸零，必須要用dampTime才不會動作突然歸零，顯示出有中斷效果
      characterController.animator.SetFloat("Vertical",0.1f,0.4f,Time.deltaTime);
      characterController.animator.SetTrigger("Jump");
      Vector3 targetDirection = input.x*characterController.cameraTransform.right + input.y*characterController.cameraTransform.forward;
        
   }

    public override void HandleInput()//會被CharacterController不斷更新
    {
        base.HandleInput();
        //可以自己加入要的jump()方法
        input =  moveAction.ReadValue<Vector2>();
        //由於這一次的動畫是用RootMotion來製作，因此就不用控制速度了，只要把input丟到animator.SetFloat就可以
        if(attackAction.triggered)
        {
            attack=true;
        }
        
    }

    public override void LogicUpdate()//會被CharacterController不斷更新,負責狀態更新
    {
      base.LogicUpdate();

      if(grounded)
      {
         stateMachine.ChangeState(characterController.standing);
      }
      if(attack)
      {
         stateMachine.ChangeState(characterController.attacking);
      }
    }

    public override void PhysicsUpdate()
    {
       base.PhysicsUpdate();
       grounded = characterController.characterIsGround;
       if(!grounded)
       {
         Debug.Log("執行降落");
         characterController.rb.AddForce(new Vector3(0,-2*20f,0),ForceMode.VelocityChange);
       }      
       
             
    }
    
}
