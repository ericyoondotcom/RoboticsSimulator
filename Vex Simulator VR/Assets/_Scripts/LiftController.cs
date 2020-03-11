using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    /// <summary>
    /// A value between 0 and 1, where 0 is all the way down and 1 is all the way up.
    /// </summary>
    public float liftPosition = 0;
    public float liftSpeed = .01f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float movement;
#if UNITY_EDITOR
        movement = Input.GetKey(KeyCode.Space) ? 1 : (Input.GetKey(KeyCode.LeftShift) ? -1 : 0);
#else
        movement = Input.GetAxis("PrimaryIndexTrigger") - Input.GetAxis("PrimaryHandTrigger");
#endif
        liftPosition += movement * liftSpeed;
        if (liftPosition < 0) liftPosition = 0;
        if (liftPosition > 1) liftPosition = 1;
    }
}
