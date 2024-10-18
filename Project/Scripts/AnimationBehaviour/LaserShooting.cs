using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : StateMachineBehaviour
{
    public GameObject laser;
    public Transform rootTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 查找父物件（替換 "ParentObjectName" 為實際的父物件名稱）
        //不管是用GameObject.Find or FindWihTag都需要是已啟動的物件(Active)，如果是SetActive(False)一開始是找不到的
        rootTransform =  animator.transform.Find("Root").transform;
        
        // 在父物件中查找名為 "Laser" 的子物件，即使它被禁用
        Transform laserTransform = rootTransform.Find("Lazer").transform;
        if (laserTransform != null)
        {
            
            laser = laserTransform.gameObject;
            laser.SetActive(true);
        }
        else
        {
            Debug.Log("沒有找到Lsaser");
        }
           
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       laser.SetActive(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
