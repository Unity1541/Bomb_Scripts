using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallDetect : MonoBehaviour
{

   public BallParameters hitParticle;
   public GameObject stars;
   //public UnityEvent OnScoreUpdated;
   //被觸發的事件,如果不是prefab就可以直接在inspector來拖曳放入，但現在是prefab一開始要先找到東西
   public UnityEvent<int,float> OnScoreUpdated;//如果加入的方法需要加入參數的時候，要先預訂變數的型態才可以
   void Start()//這邊要注意，如果有兩個以上的參數event,則inspector無法直接輸入，而是只能在Invoke自行輸入
   {
        OnScoreUpdated.AddListener(GameObject.Find("InterFace").GetComponent<ScoreSystem>().calculate);
    //這邊AddListener是方法的引用，不是調用，因此不寫calculate()。找到後等Invoke()之後再使用方法
    //同樣的分數計算也是這樣
   }
   
   public void OnCollisionEnter(Collision collision)
    {
        //所以不用管打到的gameObject是誰，以前的程式碼都是collision.gameObject.tag==A,B,C會很冗長
        //現在只關心打到的東西有沒有繼承或實作IDmage的缺口就可以了，所以打到的那個東西會執行IDamage方法
        IDamage<int> damageHP = collision.gameObject.GetComponentInChildren<IDamage<int>>();
        IRecover<float> hp= collision.gameObject.GetComponentInChildren<IRecover<float>>();
        IBool ibool = collision.gameObject.GetComponentInChildren<IBool>();

        if(damageHP==null || hp==null || ibool==null)//不寫這段會出現錯誤因為沒東西
        {
            return;
        }
        else
        {
            Vector3 generatePoint = collision.contacts[0].point;
            Instantiate(hitParticle.ballparticleSystem[0],generatePoint,Quaternion.identity);
            damageHP.PlayDamageAnimation(0);//只要被打到的東西有繼承實作interface，會自動執行方法。
            CameraShake.Instance.ControllCameraShake(2f,0.1f);//呼叫static的CameraShake.Instance方法
            
            //設定被打到
            ibool.SetBool(true);
            //計算分數,int score, and HP
            OnScoreUpdated.Invoke(1,0.1f);//是要啟動加在Event裡面的方法(也就是說使用Inspector所選擇的方法)
            //如果有方法裡面有參數，就要輸入參數,如果沒有就不用輸入參數
            Instantiate(stars,generatePoint,Quaternion.identity);
        }
        
    }

   
}
