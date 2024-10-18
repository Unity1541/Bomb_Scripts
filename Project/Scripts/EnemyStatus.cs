using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus,IDamage<int>,IRecover<float>,IBool//一旦實作接口就要給變數型態 
{
    
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

   public void PlayDamageAnimation(int hitAnimation)//繼承Interface而來
  {
      if (hitAnimation==0)
      {
            animator.SetTrigger("IsHit");
      }
       
  }

    public void RecoverHP(float hp)
    {
        
    }
    public void SetBool(bool isHit)//繼承interface而來
    {
        Debug.Log("偵測敵人到打到了");
        this.isHit = isHit;
    }
    


 
   
}
