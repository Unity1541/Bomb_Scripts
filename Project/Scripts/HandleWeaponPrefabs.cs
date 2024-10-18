using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWeaponPrefabs : MonoBehaviour
{
   
   public Camera camera;
   public TrailRenderer trailRenderer;
   public GameObject []weapons;
   public Transform shootPosition;
   public GameObject []gunWeapons;
   public Transform muzzlePosition;
   public GameObject muzzleParticle;
   public void createBall(int i)//給Animation Events呼叫丟球的動畫，在Animation Event輸入int數字
   {
     GameObject obj = Instantiate(weapons[i],shootPosition.position,Quaternion.identity);
     Destroy(obj,3f);  
   }

   public void createMuzzle()
   {
      GameObject obj = Instantiate(muzzleParticle,gunWeapons[0].transform.position,Quaternion.identity);
      Destroy(obj,1f); 
   }

   public void createBullet()
   {
      RaycastHit raycastHit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      var tracer = Instantiate(trailRenderer,gunWeapons[0].transform.position,Quaternion.identity);
      tracer.AddPosition(gunWeapons[0].transform.position);
      //tracer.transform.position = gunWeapons[0].transform.up;
      if(Physics.Raycast(ray,out raycastHit))
      {
         tracer.transform.position = raycastHit.point+new Vector3(0,0.3f,0);
      }
      
   }

   
}
