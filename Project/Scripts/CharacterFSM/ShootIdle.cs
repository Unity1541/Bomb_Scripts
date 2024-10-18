using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIdle : States
{
   bool shooting;
   bool throwAction;
   bool change;
   GameObject gunObject;
   Vector3 targetDirection;
   public ShootIdle(CharacterController _character,StateMachine _stateMachine) : base(_character,_stateMachine)
   {
        characterController = _character;
        stateMachine = _stateMachine;   
   }
   public override void Enter()
   {
        Debug.Log("Enter State: "+this.ToString());//這樣在子物件繼承時候，會先執行這邊的Enter然後再執行子物件覆蓋的Enter
        shooting = false;
        throwAction = false;
        change = false;
        input = Vector2.zero;
        characterController.animator.SetLayerWeight(2, 1);
     //    foreach (Transform child in characterController.transform)
     //    {
     //      Debug.Log("Child name: " + child.name);
     //    }
     //因為characterController是一個程式碼不是物件，不可以直接.gameObject來用
        gunObject = characterController.GetComponentInChildren<HandleWeaponPrefabs>().gunWeapons[0];
        gunObject.SetActive(true);
   }

   public override void HandleInput()
   {
        base.HandleInput();
        input =  moveAction.ReadValue<Vector2>();
        //由於只有回到走路的狀態才能旋轉位置，因此把轉動的程式碼寫在StandingState這邊。
        if(changeWeaponAction.triggered && !throwAction)
        {
          change=true;
          gunObject.SetActive(false);
        }
          

        if(shootAction.triggered)
          shooting=true;

   }

   public override void PhysicsUpdate()
   {
          Quaternion lookDirection = Quaternion.LookRotation(characterController.targetDirection,Vector3.up);
          this.characterController.transform.rotation = Quaternion.Slerp(this.characterController.transform.rotation,lookDirection,5f * Time.deltaTime);
   }

   public override void LogicUpdate()
   {
        base.LogicUpdate();
        characterController.animator.SetFloat("Horizontal",input.x,characterController.animationSpeed.speedDampTime,Time.deltaTime);
        characterController.animator.SetFloat("Vertical",input.y,characterController.animationSpeed.speedDampTime,Time.deltaTime);
        if(change)
        {
            characterController.animator.SetLayerWeight(2, 0f);
            stateMachine.ChangeState(characterController.standing);//會被CharacterContoller不斷更新
        }

        if(shooting)
        {
            Debug.Log("按下shooting");
            stateMachine.ChangeState(characterController.shootState);
        }

   }

    public override void Exit()
    {
          
    }
}
