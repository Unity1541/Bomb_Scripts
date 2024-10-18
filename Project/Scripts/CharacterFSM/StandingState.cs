using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : States
{//每一個個別狀態都會有各自要更新的參數，不會和其它狀態的參數混淆在一起
   bool attack;
   bool throwbomb;
   bool grounded;
   float playerSpeed;
   bool change;
   Vector3 targetDirection;   
  public StandingState(CharacterController _character, StateMachine _stateMachine):base(_character, _stateMachine)
  { //當創建StandingState的實例時，基類State的構造函數首先被調用並初始化character和stateMachine欄位，
    //然後StandingState的構造函數才會繼續執行並完成派生類的初始化工作。
    //如果本身base裡面沒有放函數，則不一定要放base，，不一定要寫:base(____)
    characterController = _character;//這邊是說繼承的State裡面的characterController
	stateMachine = _stateMachine;//這邊是說繼承的State裡面的stateMachine
    //因為在base的State當中，裡面有PlayerInput因此一定要更新，才能有按鍵作用，不然會按鍵沒有用

    #region 說明構造函數
        //構造函數參數:

        // CharacterController _character: 傳遞給StandingState構造函數的CharacterController物件。
        // StateMachine _stateMachine: 傳遞給StandingState構造函數的StateMachine物件。
        // base(_character, _stateMachine):
        // base(_character, _stateMachine)表示在StandingState的構造函數中，
        // 首先調用基類States的構造函數並傳遞參數。這確保了基類中的characterController、stateMachine等欄位得到正確初始化。
        
        // 構造關係:
        // 當創建StandingState的實例時，StandingState構造函數會首先調用基類States的構造函數，通過base關鍵字傳遞參數。
        // 基類States的構造函數會執行它的初始化邏輯，然後返回控制權給StandingState的構造函數。
        // StandingState構造函數繼續執行派生類的特定初始化工作。
        // 為什麼要使用 base 關鍵字
        // 初始化基類成員: base(_character, _stateMachine)確保在派生類StandingState構造函數執行之前，
        // 基類States的欄位和成員已經被初始化。這是因為基類可能包含一些必須在派生類使用之前初始化的邏輯。
        // 保持一致性: 即使基類構造函數當前不執行任何操作，調用base可以保持代碼的一致性和可維護性。未來如果基類構造函數添加了邏輯，派生類將會自動受益。
        // 參數會先給base裡面的構造函數，然後再輪到StandingState嗎
        // 基類States的構造函數會處理它自己的初始化邏輯，如設置characterController、stateMachine等欄位。
        // 此時，States類的構造函數完成其初始化任務後，控制權會返回給派生類的構造函數。
        // 調用順序解釋
        // •當執行new StandingState(_character, _stateMachine)時：
        // 1.StandingState構造函數: 首先執行StandingState構造函數，並且在其內部，base(_character, _stateMachine)被調用。
        // 2.States構造函數: base(_character, _stateMachine)調用States構造函數，初始化基類欄位characterController和stateMachine。
        // 3.返回StandingState構造函數: 一旦States構造函數完成初始化並返回，StandingState構造函數繼續執行，可以使用基類已經初始化的欄位和屬性。

    #endregion

    #region 說明
    // public BaseClass()
    // {
    //     // 預設構造函數
    // }
    // public class DerivedClass : BaseClass
    // {
    //     public DerivedClass()
    //     {
    //         // 不需要顯式調用 base()，編譯器會自動調用基類的預設構造函數
    //     }
    // }
    #endregion

  }

    public override void Enter()//進入StandingState要更新的東西
    {
        base.Enter();
        //在執行本地方法Enter()之前，先執行base本身的Enter()但是如果本身的Enter裡面是空值沒有放入東西
        //就不用寫base.Enter()了
        //發現原來的基類Enter()裡面有Debug.Log(____)
        //這樣在執行這邊的Enter()之前，會先執行基類的base.Enter()的Debug方法
        change = false;
        grounded = true;
        attack = false;
        throwbomb = false;
        input = Vector2.zero;//這邊的input是繼承State而來的
    }

    public override void HandleInput()//會被CharacterController不斷更新
    {
        base.HandleInput();
        input =  moveAction.ReadValue<Vector2>();

        //由於只有回到走路的狀態才能旋轉位置，因此把轉動的程式碼寫在StandingState這邊。
        targetDirection = input.x*characterController.cameraTransform.right + input.y*characterController.cameraTransform.forward;
        targetDirection.Normalize();
        targetDirection.y=0.0f;
   
        // if(jumpAction.triggered)//繼承State而來的jumpAction = characterController.playerInput.actions["Jump"];
        // {
        //     jump=true;
        // }
       
        //由於這一次的動畫是用RootMotion來製作，因此就不用控制速度了，只要把input丟到animator.SetFloat就可以
        if(attackAction.triggered)
        {
            attack = true;
        }
        if(throwAction.triggered && !throwbomb)
        {
            throwbomb = true;
        }
        if(changeWeaponAction.triggered && !throwbomb)
        {
            change = true;
        }

    }

    public override void LogicUpdate()//會被CharacterController不斷更新
    {
        base.LogicUpdate();
        characterController.animator.SetFloat("Horizontal",input.x,characterController.animationSpeed.speedDampTime,Time.deltaTime);
        characterController.animator.SetFloat("Vertical",input.y,characterController.animationSpeed.speedDampTime,Time.deltaTime);  
        // if(jump)
        // {
        //     stateMachine.ChangeState(characterController.jumping);//會被CharacterContoller不斷更新
        //     //進入jumpState的Enter()開始更新
        // }
        if(attack)
        {//轉向攻擊的方向
            Ray ray = characterController.camera.ScreenPointToRay(Input.mousePosition); 
            // Create a ray from the screen point
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1000))
            {
                Vector3 hitPosition = hit.point;
                Vector3 targetDirection = (hitPosition - characterController.transform.position).normalized;
                targetDirection.y=0;
                characterController.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            }
            stateMachine.ChangeState(characterController.attacking);//會被CharacterContoller不斷更新
        }
        if(throwbomb)
        {
            Vector3 targetDirection = (characterController.enemyTransform.position - characterController.transform.position).normalized;
            characterController.transform.rotation = Quaternion.LookRotation(targetDirection,Vector3.up);
            stateMachine.ChangeState(characterController.attacking);//會被CharacterContoller不斷更新
        }
        if(change)
        {   
            Debug.Log("有按下轉換紐");
            // weight += Mathf.Lerp(weight, 1.0f, 15*Time.deltaTime);
            // characterController.animator.SetLayerWeight(2, weight);
            stateMachine.ChangeState(characterController.shootIdle);//會被CharacterContoller不斷更新
        }
    
    }

    public override void PhysicsUpdate()//會被CharacterController不斷更新
    {
        base.PhysicsUpdate();
        if (targetDirection.sqrMagnitude>0 && input.x>=0)
        //可以直接透過腳本直接取得掛有該腳本物件的Transform,input.x>0表示不可以往後轉,如果input.x<0就是撥放root向後走
		{//squaredLength值將始終是非負的，因為平方運算會將所有的數字（正數或負數）轉換為非負數。因此，不存在sqrMagnitude為負值的情況。
            characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation, Quaternion.LookRotation(targetDirection,Vector3.up),characterController.animationSpeed.speedDampTime);
        }
        else if(input.y!=0 && input.x!=0)//把原來的input.y<=0改掉，就用input.y 和 input.x!=0足以代表全部現象
        {
            characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation,Quaternion.Euler(0,90,0),characterController.animationSpeed.speedDampTime);   
        }
        else if(input.y>=0 && input.x!=0)
        {   
            characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation,Quaternion.Euler(0,90,0),characterController.animationSpeed.speedDampTime);
        }
    }
      
        
    public override void Exit()
    {
        base.Exit();
    }


}
