using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage<T>//表示等等實作要放入任一種參數int or float都可以
{
    void PlayDamageAnimation(T damage);
}
//例如等等被實作的話
//public class Boss : IDemage<float>
// public void TakeDmage(float damage)
//public class Enemy : Idemage<int>
// public void TakeDamage(int damage)     

public interface IRecover<T>
{
    void RecoverHP(T hp);
}

public interface IBool
{
    // bool IsHit(bool hitStatus); // Method signature that returns a boolean
    void SetBool(bool isHit);
}
