using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallFly : MonoBehaviour
{
    public BallParameters enemyBallParameters;
    public Vector3 flyDiretion;
    private Rigidbody rb;
    void Start()
    {
        enemyBallParameters.targetTransfom[0] = GameObject.Find("Player").transform;
        enemyBallParameters.targetTransfom[1] = GameObject.Find("ShootingPosition").transform;
        rb = GetComponent<Rigidbody>();

        flyDiretion = (enemyBallParameters.targetTransfom[0].position - enemyBallParameters.targetTransfom[1].position).normalized;
        flyDiretion.y=0;
        rb.AddForce((flyDiretion+new Vector3(0,0.34f,0f))*enemyBallParameters.ball_Speed,ForceMode.Impulse);
        //要往上加0.34不然，球無法跨界，另外球速度也會影響拋物線高度，因為是用Addforce的物理特性，所以speed不可以太快 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
