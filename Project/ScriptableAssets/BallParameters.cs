using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ball/BallParameters")]
public class BallParameters : ScriptableObject
{
   public Transform[] targetTransfom;
   public GameObject[] ballparticleSystem;
   public float ball_Speed;
   public float ball_Damage;
   public float flyTime;
   public AnimationCurve animationCurve;
   public bool isHit;
   public string object_name;
}
