using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParameter : StateMachineBehaviour
{
    // public string targetParameter;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public EnemyStatus enemyStatus;
    public PlayerStatus playerStatus;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        if (enemyStatus==null)
             return;
        if (playerStatus==null)
             return;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if (enemyStatus!=null)
            enemyStatus.isHit = false;
        if (playerStatus!=null)
            playerStatus.isHit = false;
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
