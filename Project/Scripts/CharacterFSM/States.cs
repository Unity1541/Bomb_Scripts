using UnityEngine;
using UnityEngine.InputSystem;

public class States//先定義所需要的控制器以及用到狀態，和會用到的方法
{
    public CharacterController characterController;
    public StateMachine stateMachine;
    public InputAction moveAction;
    public InputAction lookAction;
    //public InputAction jumpAction;
    public InputAction shootAction;
    public InputAction changeWeaponAction;
    public InputAction attackAction;
    public InputAction throwAction;
    public Vector2 input;
    public Vector3 gravityVelocity;
    public Vector3 velocity;
    public States(CharacterController _character, StateMachine _stateMachine)
	{
        characterController = _character;
        stateMachine = _stateMachine;
        moveAction = characterController.playerInput.actions["Move"];
        //jumpAction = characterController.playerInput.actions["Jump"];
        attackAction = characterController.playerInput.actions["Attack"];
        throwAction = characterController.playerInput.actions["Throw"];
        changeWeaponAction = characterController.playerInput.actions["ChangeLayer"];
        shootAction = characterController.playerInput.actions["Shoot"]; 

    }
#region 說明
    //注意這邊是構造函數，先定義一個Class States
    //然後該類別有同樣名字的States函數，裡面要放東西
    //範例public class Person
    // {
    //     public string Name;
    //     public int Age;
    //     public Person(string name, int age)//構造函數
    //     {
    //         Name = name; // 初始化Name字段
    //         Age = age;   // 初始化Age字段
    //     }
    //     // 其他方法
    //     public void DisplayInfo()
    //     {
    //         Console.WriteLine($"Name: {Name}, Age: {Age}");
    //     }
    // }
    // // 使用構造函数創建實例
    // Person person = new Person("Alice", 30);
    // person.DisplayInfo();Name: Alice, Age: 30
#endregion
    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
		Debug.Log("Enter State: "+this.ToString());//這樣在子物件繼承時候，會先執行這邊的Enter然後再執行子物件覆蓋的Enter
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
