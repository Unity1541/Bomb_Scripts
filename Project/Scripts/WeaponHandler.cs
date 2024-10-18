using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Camera camera;
    public Transform rayStart;
    public BallParameters hitParticle;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rayStart = GameObject.Find("Player").transform;
    }
    void GunParticle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1000))
            {
                Vector3 hitPosition = hit.point;
                if (hit.collider.name =="Doreamon")
                {
                    IDamage<int> damageHP =hit.collider.gameObject.GetComponentInChildren<IDamage<int>>();
                    damageHP.PlayDamageAnimation(0);//只要被打到的東西有繼承實作interface，會自動執行方法。
                    GameObject obj = Instantiate(hitParticle.ballparticleSystem[0],hitPosition,Quaternion.identity);
                    Destroy(obj,0.15f);
                }
            }
       
    }
}
