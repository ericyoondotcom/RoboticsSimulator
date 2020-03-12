using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Claw : MonoBehaviour
{
    bool closingClaw = false;
    public float clawAnimTime;
    public Vector3 rotationStart;
    public Vector3 rotationEnd;
    public Transform leftClaw;
    public Transform rightClaw;
    float t = 0;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            closingClaw = true;
            t = 0;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            closingClaw = false;
            t = 0;
        }
#else
        if (Input.GetAxis("SecondaryIndexTrigger"))
        {
            closingClaw = true;
        }
        else if (Input.GetAxis("SecondaryHandTrigger"))
        {
            closingClaw = false;
        }
#endif

        if (closingClaw)
        {
            leftClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(rotationStart), Quaternion.Euler(rotationEnd), t);
            rightClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(-rotationStart), Quaternion.Euler(-rotationEnd), t);
        }
        else
        {
            leftClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(rotationEnd), Quaternion.Euler(rotationStart), t);
            leftClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(-rotationEnd), Quaternion.Euler(-rotationStart), t);
        }
        t += Time.fixedDeltaTime;
        
    }
}
