using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void OnAnimatorMove()//這邊是為了要讓角色和Rigidbody同時移動不然Capusle會停留在原地
    {
        float delta = Time.deltaTime;
        Vector3 deltaPosition =animator.deltaPosition;
        deltaPosition.y =0.0f;
        Vector3 velocity = deltaPosition/delta;
        rb.velocity = velocity;
    }
}
