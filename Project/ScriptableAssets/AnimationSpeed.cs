using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableAnimations/AnimationSpeed")]
public class AnimationSpeed : ScriptableObject
{
    [Range(0,1f)]
    public float speedDampTime;
    [Range(0,1f)]
    public float velocityTime;
    [Range(0,2f)]
    public float animationSpeed;
    [Range(0,1.5f)]
    public float hitStopTime;
}
