using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class LiftAnimationController : MonoBehaviour
{
    Animation animation;
    void Start()
    {
        animation = GetComponent<Animation>();
        animation.Play();
    }

   void Update()
    {
        
    }
}
