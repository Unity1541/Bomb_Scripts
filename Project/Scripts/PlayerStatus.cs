using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus,IDamage<int>,IRecover<float>,IBool//一旦實作接口就要給變數型態 
{
    public Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

  
  public void PlayDamageAnimation(int hitAnimation)
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
        Debug.Log("偵測到打到了");
        this.isHit = isHit;
  }
}
