using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localRotation *= Quaternion.Euler(0, 0, 30f);
    }
}
