using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFly : MonoBehaviour
{
    public Rigidbody rb;
    public Camera camera;
    public Transform flyTransform;
    //public Transform endPosition;
    //public Transform controlPosition;
    public BallParameters ballParameters;
    void Start()
    { 
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        flyTransform = GameObject.Find("FlyDirection").transform;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition); 
            // Create a ray from the screen point
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1000))
            {
                Vector3 hitPosition = hit.point;//球飛出的方向滑鼠點到的方向和flyTransform的向量
                Vector3 targetDirection = (hitPosition - flyTransform.position).normalized;
                targetDirection.y=0;
                rb.AddForce((targetDirection+new Vector3(0,0.36f,0.2f))*ballParameters.ball_Speed,ForceMode.Impulse);
                //z軸方向要修正，這樣才可以準確打到，滑鼠點擊的目標
            }
        
        //endPosition = GameObject.Find("Enemy").transform;
        //controlPosition = GameObject.Find("CurveMiddlePoint").transform;
        //rb.AddForce((flyTransform.forward+new Vector3(0,0.34f,0))*ballParameters.ball_Speed,ForceMode.Impulse);     
    }


    // public Vector3 evaluateCurve(float time)
    // {
    //     Vector3 ac = Vector3.Lerp(flyTransform.position,controlPosition.position,time);
    //     Vector3 cb = Vector3.Lerp(controlPosition.position,endPosition.position,time);
    //     return Vector3.Lerp(ac,cb,time);
    //     //因為線性差值Lerp是利用(1-t)*a + t*b, 因此隨著時間越大，終點b應該要權重要越大，如果顛倒則路徑顛倒
    // }


    void Update()
    {
        ballParameters.flyTime += Time.deltaTime*ballParameters.ball_Speed;
        
        // Vector3 result = evaluateCurve(ballParameters.flyTime);
        // //this.transform.position = result;
        // Vector3 direction =(evaluateCurve(ballParameters.flyTime + 0.01f) - transform.position).normalized;
        // this.transform.forward = evaluateCurve(ballParameters.flyTime)-transform.position;
    }
}
