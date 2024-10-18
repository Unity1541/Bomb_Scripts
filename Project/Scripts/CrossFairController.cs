using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CrossFairController : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform crossFair;
    public Camera cam;
    public bool isDetectEnemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo,200,layerMask))
        {
            crossFair.position = hitInfo.point;
        }
    }
}
