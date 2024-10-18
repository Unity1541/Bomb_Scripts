using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyWeapons : MonoBehaviour
{
    public GameObject []weaponObjects;
    public Transform []weaponShooting;
    public int weaponIndex;//放到Animation Event會有一格int要你填入數字

    public void WeaponsCreate(int weaponIndex)
    {
        GameObject gameObject = Instantiate(weaponObjects[weaponIndex],weaponShooting[0].position,Quaternion.identity);
        Destroy(gameObject,2.0f);
    }

}
