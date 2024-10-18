using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFly : MonoBehaviour
{
    public Rigidbody rb;
    public Camera camera;
    public Transform flyTransform;
    
    public BallParameters ballParameters;
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        flyTransform = GameObject.Find("FlyDirection").transform;
        rb.velocity = (ballParameters.ball_Speed * flyTransform.forward)+new Vector3(0,4f,0);
              
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
